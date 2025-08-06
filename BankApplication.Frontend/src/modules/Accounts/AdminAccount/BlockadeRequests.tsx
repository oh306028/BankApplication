import { useEffect, useState } from "react";
import AccountsService, {
  type BlockRequestDetails,
  type BlockRequestModel,
} from "../AccountsService";
import dayjs from "dayjs";
import styles from "../../../styles/BlockadeRequests.module.css";

function BlockadeRequests() {
  const [activeRequests, setActiveRequests] = useState<BlockRequestDetails[]>(
    []
  );
  const [historicalRequests, setHistoricalRequests] = useState<
    BlockRequestDetails[]
  >([]);
  const [isLoading, setIsLoading] = useState(true);

  const fetchData = async () => {
    setIsLoading(true);
    try {
      const result = await AccountsService.getBlockRequests();
      const active = result.filter((r) => r.isActive);
      const history = result
        .filter((r) => !r.isActive)
        .sort(
          (a, b) =>
            new Date(b.managedDate).getTime() -
            new Date(a.managedDate).getTime()
        );

      setActiveRequests(active);
      setHistoricalRequests(history);
    } catch (error) {
      console.error("Błąd podczas pobierania wniosków o blokadę:", error);
    } finally {
      setIsLoading(false);
    }
  };

  useEffect(() => {
    fetchData();
  }, []);

  const handleManageRequest = async (
    request: BlockRequestDetails,
    accept: boolean
  ) => {
    const model: BlockRequestModel = {
      publicId: request.publicId,
      accepted: accept,
    };
    try {
      await AccountsService.manageBlockRequest(model, request.accountId);
      await fetchData();
    } catch {
      console.log("Wystapil blad!");
    }
  };

  if (isLoading) {
    return <p className={styles.placeholder}>Ładowanie danych...</p>;
  }

  return (
    <div className={styles.listContainer}>
      <div className={styles.section}>
        <h2 className={styles.title}>Wnioski</h2>
        {activeRequests.length > 0 ? (
          <ul className={styles.requestList}>
            {activeRequests.map((req, index) => (
              <li key={index} className={styles.requestItem}>
                <div className={styles.requestInfo}>
                  <p className={styles.clientName}>{req.clientName}</p>
                  <span className={styles.requestDate}>
                    {req.bankAccountNumber}
                  </span>
                  <br></br>
                  <span className={styles.requestDate}>
                    Złożono: {dayjs(req.requestDate).format("DD.MM.YYYY HH:mm")}
                  </span>
                </div>
                <div className={styles.actionButtons}>
                  <button
                    onClick={() => handleManageRequest(req, true)}
                    className={`${styles.actionButton} ${styles.acceptButton}`}
                  >
                    ✔
                  </button>
                  <button
                    onClick={() => handleManageRequest(req, false)}
                    className={`${styles.actionButton} ${styles.rejectButton}`}
                  >
                    ✖
                  </button>
                </div>
              </li>
            ))}
          </ul>
        ) : (
          <p className={styles.placeholder}>Brak aktywnych wniosków.</p>
        )}
      </div>

      <hr className={styles.separator} />

      <div className={styles.section}>
        <h2 className={styles.title}>Historia wniosków</h2>
        {historicalRequests.length > 0 ? (
          <ul className={styles.requestList}>
            {historicalRequests.map((req, index) => (
              <li
                key={index}
                className={`${styles.requestItem} ${styles.historyItem}`}
              >
                <div className={styles.requestInfo}>
                  <p className={styles.clientName}>{req.clientName}</p>
                  <span className={styles.requestDate}>
                    Złożono: {dayjs(req.requestDate).format("DD.MM.YYYY")}
                  </span>
                </div>
                <div className={styles.historyDetails}>
                  <span
                    className={`${styles.statusBadge} ${
                      req.isAccepted
                        ? styles.statusAccepted
                        : styles.statusRejected
                    }`}
                  >
                    {req.isAccepted ? "Zaakceptowano" : "Odrzucono"}
                  </span>
                  <span className={styles.managedDate}>
                    {dayjs(req.managedDate).format("DD.MM.YYYY HH:mm")}
                  </span>
                </div>
              </li>
            ))}
          </ul>
        ) : (
          <p className={styles.placeholder}>Brak historii wniosków.</p>
        )}
      </div>
    </div>
  );
}

export default BlockadeRequests;
