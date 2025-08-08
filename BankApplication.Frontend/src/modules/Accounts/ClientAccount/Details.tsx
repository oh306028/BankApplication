import { useEffect, useState } from "react";
import AccountsService, {
  type KeyValuePair,
  type Details as AccountDetails,
} from "../AccountsService";
import ClientNavBar from "./ClientNavBar";
import SendTransfer from "../Tranfers/SendTransfer";
import Modal from "../../../modals/AlertModal";
import TransferList from "../Tranfers/TransferList";
import styles from "../../../styles/Details.module.css";
import TransferService from "../Tranfers/TransferService";
import Footer from "../../../Footer";
import Picker from "./Picker";

type HistoryFilter = "all" | "sent" | "received";

function Details() {
  const [ownTypes, setOwnTypes] = useState<KeyValuePair[]>([]);
  const [details, setDetails] = useState<AccountDetails | null>(null);
  const [selectedAccountType, setSelectedAccountType] = useState<string | null>(
    null
  );
  const [isTransferActive, setIsTransferActive] = useState<boolean>(false);
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [modalMessage, setModalMessage] = useState("");
  const [historyFilter, setHistoryFilter] = useState<HistoryFilter>("all");
  const [showHistory, setShowHistory] = useState<boolean>(false);
  const [transferListVersion, setTransferListVersion] = useState(0);
  const [hasActiveBlockRequests, setHasActiveBlockRequests] =
    useState<boolean>(false);
  const [showPicker, setShowPicker] = useState<boolean>(false);
  const [isBlocked, setIsBlocked] = useState<boolean>(false);

  useEffect(() => {
    const getInitialData = async () => {
      const result = await AccountsService.getOwnTypes();
      setOwnTypes(result);
      if (result.length > 0) {
        setSelectedAccountType(result[0].value);
      }
    };
    getInitialData();
  }, []);

  useEffect(() => {
    const fetchDetails = async () => {
      if (!selectedAccountType) return;
      try {
        const result = await AccountsService.getDetailsByType(
          selectedAccountType
        );

        const isBlocked = await AccountsService.isBlocked(result.publicId);
        setIsBlocked(isBlocked);

        setDetails(result);
        setIsTransferActive(false);
        setShowHistory(false);
        setHistoryFilter("all");
      } catch (error) {
        setDetails(null);
      }
    };
    fetchDetails();
  }, [selectedAccountType]);

  useEffect(() => {
    const checkBlockRequests = async () => {
      if (details?.publicId) {
        const result = await AccountsService.hasBLockRequests(details.publicId);
        setHasActiveBlockRequests(result || isBlocked);
      }
    };
    checkBlockRequests();
  }, [details]);

  const refreshPageData = async () => {
    if (!selectedAccountType) return;
    try {
      const types = await AccountsService.getOwnTypes();
      setOwnTypes(types);

      const result = await AccountsService.getDetailsByType(
        selectedAccountType
      );
      setDetails(result);
      setShowPicker(false);
    } catch (error) {
      console.error("Błąd podczas odświeżania danych:", error);
    }
  };

  const toggleTransfer = () => setIsTransferActive(!isTransferActive);
  const showModal = (message: string) => {
    setModalMessage(message);
    setIsModalOpen(true);
  };

  const handleTransferSent = async () => {
    if (!selectedAccountType) return;
    const result = await AccountsService.getDetailsByType(selectedAccountType);
    setDetails(result);
    showModal("Przelew został wykonany pomyślnie");
    toggleTransfer();
    setTransferListVersion((currentVersion) => currentVersion + 1);
  };

  const handleDownloadHistory = async () => {
    if (!details) {
      showModal("Nie można pobrać historii, brak danych konta.");
      return;
    }
    try {
      await TransferService.downloadTransfersPdf(details.publicId);
    } catch (error) {
      showModal("Błąd podczas pobierania historii przelewów.");
    }
  };

  const handleDownloadDetails = async () => {};
  const handleAccountPicker = async () => {
    if (ownTypes.length === 3) {
      showModal("Utworzono już wszystkie typy rachunków.");
      return;
    }
    setShowPicker(!showPicker);
  };

  const handleBlockRequest = async () => {
    if (hasActiveBlockRequests) {
      if (isBlocked) {
        showModal(
          "Rachunek zablokowany. Skontaktuj się z administratorem w celu odblokowania."
        );
        return;
      }

      showModal("Wysłano już wniosek. Oczekiwanie na akceptację.");
      return;
    }

    if (!details) {
      showModal("Nie można przesłać wniosku o blokadę, brak danych konta.");
      return;
    }
    try {
      await AccountsService.sendBlockRequest(details.publicId);
      setHasActiveBlockRequests(true);
      showModal("Wysłano wniosek o blokadę konta do administratora.");
    } catch (error) {
      showModal("Błąd podczas wysyłania wniosku.");
    }
  };

  return (
    <>
      {ownTypes && showPicker && (
        <Picker
          onCancelPicking={() => setShowPicker(false)}
          hasAccount={true}
          onAccountCreated={refreshPageData}
          ownTypes={ownTypes}
        />
      )}
      {!showPicker && (
        <>
          <ClientNavBar />
          <div className={styles.detailsPage}>
            <div className={styles.container}>
              <div className={styles.accountsPanel}>
                <h2 className={styles.panelTitle}>Twoje konta</h2>
                <ul className={styles.accountsList}>
                  {ownTypes.map((p) => (
                    <li
                      onClick={() => setSelectedAccountType(p.value)}
                      key={p.key}
                      className={
                        selectedAccountType === p.value
                          ? styles.activeAccount
                          : ""
                      }
                    >
                      {p.name}
                    </li>
                  ))}
                </ul>
              </div>
              <button
                onClick={handleAccountPicker}
                className={
                  ownTypes.length === 3
                    ? styles.disabledActionButton
                    : styles.actionButton
                }
              >
                Dodaj nowe konto
              </button>
              <button
                className={
                  hasActiveBlockRequests
                    ? styles.disabledActionButton
                    : styles.actionButton
                }
                onClick={handleBlockRequest}
              >
                Wniosek o blokadę konta
              </button>
            </div>

            {isBlocked && (
              <div className={styles.blockedOverlay}>
                <div className={styles.blockedContent}>
                  <span className={styles.blockedIcon}>🔒</span>
                  <h2 className={styles.blockedTitle}>Rachunek zablokowany</h2>
                  <p className={styles.blockedMessage}>
                    Twój wniosek został zaakceptowany. Skontaktuj się z
                    administratorem w celu odblokowania rachunku.
                  </p>
                </div>
              </div>
            )}
            {!isBlocked && (
              <>
                <div className={styles.mainContent}>
                  {details ? (
                    <>
                      <div className={styles.detailsBox}>
                        <div className={styles.balanceInfo}>
                          <p className={styles.balanceLabel}>Dostępne środki</p>
                          <p className={styles.balanceAmount}>
                            {details.balance.toFixed(2)}
                            <span className={styles.currency}>
                              {" "}
                              {details.currency}
                            </span>
                          </p>
                        </div>
                        <div className={styles.accountNumberInfo}>
                          <p>Numer konta</p>
                          <p className={styles.accountNumber}>
                            {details.accountNumber}
                          </p>
                        </div>
                        <div className={styles.container}>
                          <button
                            onClick={toggleTransfer}
                            className={styles.actionButton}
                          >
                            {isTransferActive
                              ? "Anuluj przelew"
                              : "Nowy przelew"}
                          </button>

                          <button
                            onClick={handleDownloadDetails}
                            className={styles.downloadButton}
                          >
                            Pobierz wyciąg
                          </button>
                        </div>
                      </div>

                      {isTransferActive && (
                        <SendTransfer
                          publicId={details.publicId}
                          onTransferSent={handleTransferSent}
                        />
                      )}

                      <div className={styles.historySection}>
                        <h3
                          onClick={() => setShowHistory(!showHistory)}
                          className={styles.historyTitle}
                        >
                          Historia transakcji
                          <span
                            className={`${styles.arrow} ${
                              showHistory ? styles.arrowUp : ""
                            }`}
                          ></span>
                        </h3>
                        {showHistory && (
                          <>
                            <div className={styles.toolbar}>
                              <div className={styles.filterButtons}>
                                <button
                                  onClick={() => setHistoryFilter("all")}
                                  className={
                                    historyFilter === "all"
                                      ? styles.activeFilter
                                      : ""
                                  }
                                >
                                  Wszystkie
                                </button>
                                <button
                                  onClick={() => setHistoryFilter("received")}
                                  className={
                                    historyFilter === "received"
                                      ? styles.activeFilter
                                      : ""
                                  }
                                >
                                  Przychodzące
                                </button>
                                <button
                                  onClick={() => setHistoryFilter("sent")}
                                  className={
                                    historyFilter === "sent"
                                      ? styles.activeFilter
                                      : ""
                                  }
                                >
                                  Wychodzące
                                </button>
                              </div>
                              <button
                                onClick={handleDownloadHistory}
                                className={styles.downloadButton}
                              >
                                Pobierz historię
                              </button>
                            </div>

                            <TransferList
                              key={transferListVersion}
                              publicId={details.publicId}
                              accountNumber={details.accountNumber}
                              filter={historyFilter}
                            />
                          </>
                        )}
                      </div>
                    </>
                  ) : (
                    <p>Ładowanie danych konta...</p>
                  )}
                </div>
              </>
            )}
          </div>
          <Modal isOpen={isModalOpen} onClose={() => setIsModalOpen(false)}>
            <p>{modalMessage}</p>
          </Modal>
          <Footer />
        </>
      )}
    </>
  );
}

export default Details;
