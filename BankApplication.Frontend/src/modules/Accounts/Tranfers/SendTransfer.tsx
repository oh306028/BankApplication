import React, { useState } from "react";
import type { Form } from "./TransferService";
import TransferService from "./TransferService";
import AlertModal from "../../../modals/AlertModal.tsx";
import styles from "../../../styles/SendTransfer.module.css";

interface SendTransferProps {
  publicId: string;
  onTransferSent: () => Promise<void>;
}

const SendTransfer: React.FC<SendTransferProps> = ({
  publicId,
  onTransferSent,
}) => {
  const [formData, setFormData] = useState<Form>({
    accountToNumber: "",
    amount: 0,
    title: "",
    description: "",
  });

  const [isModalOpen, setIsModalOpen] = useState(false);
  const [modalMessage, setModalMessage] = useState("");

  const showModal = (message: string) => {
    setModalMessage(message);
    setIsModalOpen(true);
  };

  const handleChange = (
    e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>
  ) => {
    const { name, value } = e.target;
    setFormData((prevData) => ({
      ...prevData,
      [name]: name === "amount" ? parseFloat(value) || 0 : value,
    }));
  };

  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();

    if (!formData.accountToNumber.trim()) {
      showModal("Proszę podać numer konta odbiorcy.");
      return;
    }

    if (!/^\d{26}$/.test(formData.accountToNumber)) {
      showModal("Numer konta musi składać się z 26 cyfr.");
      return;
    }
    if (formData.amount <= 0) {
      showModal("Kwota przelewu musi być większa od zera.");
      return;
    }
    if (!formData.title.trim()) {
      showModal("Tytuł przelewu jest wymagany.");
      return;
    }

    try {
      await TransferService.send(publicId, formData);
      await onTransferSent();

      setFormData({
        accountToNumber: "",
        amount: 0,
        title: "",
        description: "",
      });
    } catch (ex: any) {
      if (ex.status === 404) {
        showModal(ex.response.data);
      } else {
        showModal(
          "Wystąpił błąd podczas wysyłania przelewu. Spróbuj ponownie."
        );
      }
    }
  };

  return (
    <>
      <div className={styles.transferContainer}>
        <h2 className={styles.title}>Wyślij nowy przelew</h2>
        <form onSubmit={handleSubmit} className={styles.transferForm}>
          <div className={styles.formGroup}>
            <label htmlFor="accountToNumber">Numer konta odbiorcy</label>
            <input
              type="text"
              id="accountToNumber"
              name="accountToNumber"
              value={formData.accountToNumber}
              onChange={handleChange}
              placeholder="Wprowadź 26 cyfr"
              maxLength={26}
            />
          </div>

          <div className={styles.formGroup}>
            <label htmlFor="amount">Kwota</label>
            <input
              type="number"
              id="amount"
              name="amount"
              value={formData.amount === 0 ? "" : formData.amount}
              onChange={handleChange}
              placeholder="0,00"
              step="0.01"
              min="0"
            />
          </div>

          <div className={styles.formGroup}>
            <label htmlFor="title">Tytuł przelewu</label>
            <input
              type="text"
              id="title"
              name="title"
              value={formData.title}
              onChange={handleChange}
              placeholder="Np. Opłata za fakturę"
            />
          </div>

          <div className={styles.formGroup}>
            <label htmlFor="description">Opis (opcjonalnie)</label>
            <textarea
              id="description"
              name="description"
              value={formData.description}
              onChange={handleChange}
              rows={3}
              placeholder="Dodatkowe informacje dla odbiorcy"
            />
          </div>

          <button type="submit" className={styles.submitButton}>
            Wyślij przelew
          </button>
        </form>
      </div>

      <AlertModal isOpen={isModalOpen} onClose={() => setIsModalOpen(false)}>
        <p>{modalMessage}</p>
      </AlertModal>
    </>
  );
};

export default SendTransfer;
