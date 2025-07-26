import React from "react";
import styles from "../styles/AlertModal.module.css";

interface AlertModalProps {
  isOpen: boolean;
  onClose: () => void;
  children: React.ReactNode;
}

const AlertModal: React.FC<AlertModalProps> = ({
  isOpen,
  onClose,
  children,
}) => {
  if (!isOpen) {
    return null;
  }

  const handleModalContentClick = (e: React.MouseEvent) => {
    e.stopPropagation();
  };

  return (
    <div className={styles.modalBackdrop} onClick={onClose}>
      <div className={styles.modalContent} onClick={handleModalContentClick}>
        <button className={styles.closeButton} onClick={onClose}>
          ×
        </button>
        {children}
      </div>
    </div>
  );
};

export default AlertModal;
