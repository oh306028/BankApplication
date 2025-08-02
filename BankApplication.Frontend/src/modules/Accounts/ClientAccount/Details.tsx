import { useEffect, useState } from "react";
import AccountsService, {
  type KeyValuePair,
  type Details,
} from "../AccountsService";
import ClientNavBar from "./ClientNavBar";
import SendTransfer from "../Tranfers/SendTransfer";
import Modal from "../../../modals/AlertModal.tsx";
import TransferList from "../Tranfers/TransferList.tsx";
import ReceivedTransfers from "../Tranfers/ReceivedTransfers.tsx";
import SentTransfers from "../Tranfers/SentTransfers.tsx";

function Details() {
  const [ownTypes, setOwnTypes] = useState<KeyValuePair[]>([]);
  const [details, setDetails] = useState<Details>();
  const [isTransferActive, setIsTransferActive] = useState<boolean>(false);
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [modalMessage, setModalMessage] = useState("");
  const [onlySentTransfers, setOnlySentTransfers] = useState<boolean>(false);
  const [onlyReceivedTransfers, setOnlyReceivedTransfers] =
    useState<boolean>(false);

  const [showHistory, setShowHistory] = useState<boolean>(false);
  let publicId = "";

  const getOwnTypes = async () => {
    const result = await AccountsService.getOwnTypes();
    setOwnTypes(result);
  };

  const fetchDetails = async (type: string) => {
    const result = await AccountsService.getDetailsByType(type);
    publicId = result.publicId;
    setDetails(result);
  };

  const toggleTransfer = () => {
    setIsTransferActive(!isTransferActive);
  };

  useEffect(() => {
    getOwnTypes();
  }, []);

  const showModal = (message: string) => {
    setModalMessage(message);
    setIsModalOpen(true);
  };

  useEffect(() => {
    if (ownTypes.length > 0) {
      fetchDetails(ownTypes[0].value);
    }
  }, [ownTypes]);

  const refreshAccounts = async () => {
    fetchDetails(ownTypes[0].value);
    showModal("Przelew został wykonany pomyślnie");
    toggleTransfer();
  };

  return (
    <>
      <ClientNavBar />
      <div>
        <h1>Moje konta bankowe</h1>
        <div>
          <ul>
            {ownTypes.map((p) => (
              <li onClick={() => fetchDetails(p.value)} key={p.key}>
                {p.name}
              </li>
            ))}
          </ul>
          <div>
            <p>
              Numer konta:<b>{details?.accountNumber}</b>
            </p>
            <p>
              Saldo:{" "}
              <b>
                {details?.balance.toFixed(2)}
                <span> {details?.currency}</span>
              </b>
            </p>
            <button onClick={toggleTransfer}>Nowy przelew</button>
            {isTransferActive && (
              <SendTransfer
                publicId={details?.publicId}
                onTransferSent={refreshAccounts}
              />
            )}
          </div>
        </div>

        <div>
          <h4 onClick={() => setShowHistory(!showHistory)}>
            Historia transakcji
          </h4>
          {showHistory && (
            <>
              {" "}
              <div
                onClick={() => setOnlyReceivedTransfers(!onlyReceivedTransfers)}
              >
                Przychodzące
              </div>
              <div onClick={() => setOnlySentTransfers(!onlySentTransfers)}>
                Wychodzące
              </div>
              {!onlyReceivedTransfers &&
                !onlySentTransfers &&
                details?.publicId && (
                  <TransferList publicId={details?.publicId} />
                )}
              {onlyReceivedTransfers && <ReceivedTransfers />}
              {onlySentTransfers && <SentTransfers />}
            </>
          )}
        </div>
      </div>

      <Modal isOpen={isModalOpen} onClose={() => setIsModalOpen(false)}>
        <h3 style={{ marginBottom: "15px" }}>Uwaga</h3>
        <p>{modalMessage}</p>
      </Modal>
    </>
  );
}

export default Details;
