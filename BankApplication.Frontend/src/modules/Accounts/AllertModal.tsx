import React from "react";
import styles from "../../styles/AllertModal.module.css";

interface AllertModalProps {
  isOpen: boolean;
  onClose: () => void;
  children: React.ReactNode;
}

const AllertModal: React.FC<AllertModalProps> = ({
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

export default AllertModal;
