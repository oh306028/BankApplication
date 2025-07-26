import { useEffect, useState } from "react";
import AccountsService from "../AccountsService.ts";
import Picker from "./Picker.tsx";
import Modal from "../../../modals/AlertModal.tsx";
import Details from "./Details.tsx";

function ClientAccounts() {
  const [hasAccounts, setHasAccounts] = useState<boolean>(false);
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [modalMessage, setModalMessage] = useState("");

  useEffect(() => {
    const checkAdmin = async () => {
      const result = await AccountsService.hasBankAccounts();

      setHasAccounts(result);
    };

    checkAdmin();
  }, []);

  const showModal = (message: string) => {
    setModalMessage(message);
    setIsModalOpen(true);
  };

  const refreshAccounts = async () => {
    const result = await AccountsService.hasBankAccounts();
    setHasAccounts(result);
    showModal("Konto zostało pomyślnie utworzone!");
  };

  return (
    <>
      {hasAccounts ? (
        <Details />
      ) : (
        <Picker onAccountCreated={refreshAccounts} />
      )}
      <Modal isOpen={isModalOpen} onClose={() => setIsModalOpen(false)}>
        <h3 style={{ marginBottom: "15px" }}>Uwaga</h3>
        <p>{modalMessage}</p>
      </Modal>
    </>
  );
}

export default ClientAccounts;
