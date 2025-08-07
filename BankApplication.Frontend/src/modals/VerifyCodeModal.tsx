import React, { useState } from "react";
import styles from "../styles/VerifyCodeModal.module.css";
import { useNavigate } from "react-router";
import type { VerifyCodeModel } from "../modules/Authentication/AuthenticationService";
import AuthenticationService from "../modules/Authentication/AuthenticationService";

interface VerifyCodeModalProps {
  isOpen: boolean;
  onClose?: () => void;
  loginAttemptId: number;
  children?: React.ReactNode;
}

const VerifyCodeModal: React.FC<VerifyCodeModalProps> = ({
  isOpen,
  onClose,
  children,
  loginAttemptId,
}) => {
  const [verificationCode, setVerificationCode] = useState("");

  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const navigate = useNavigate();

  if (!isOpen) {
    return null;
  }

  const handleModalContentClick = (e: React.MouseEvent) => {
    e.stopPropagation();
  };

  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    setError(null);

    if (!verificationCode.trim()) {
      setError("Proszę wprowadzić kod weryfikacyjny.");
      return;
    }

    setIsLoading(true);
    const verificationForm: VerifyCodeModel = {
      verificationCode: verificationCode,
      loginAttemptId: loginAttemptId,
    };

    try {
      const response = await AuthenticationService.verifyCode(verificationForm);
      localStorage.setItem("token", response);
      navigate("/bankAccounts");
    } catch (err: any) {
      setError("Wprowadzony kod jest nieprawidłowy. Spróbuj ponownie.");
      console.error("Błąd weryfikacji:", err);
    } finally {
      setIsLoading(false);
    }
  };

  return (
    <div className={styles.modalBackdrop} onClick={onClose}>
      <div className={styles.modalContent} onClick={handleModalContentClick}>
        <h3 className={styles.title}>Weryfikacja dwuetapowa</h3>
        <div className={styles.message}>
          {children ||
            "Na Twój adres email został wysłany kod weryfikacyjny. Wprowadź go poniżej."}
        </div>

        <form onSubmit={handleSubmit}>
          <div className={styles.formGroup}>
            <input
              type="text"
              value={verificationCode}
              onChange={(e) => setVerificationCode(e.target.value)}
              className={styles.input}
              maxLength={7}
            />
          </div>

          {error && <p className={styles.errorMessage}>{error}</p>}

          <button
            type="submit"
            disabled={isLoading}
            className={styles.submitButton}
          >
            {isLoading ? "Weryfikowanie..." : "Weryfikuj"}
          </button>
        </form>
      </div>
    </div>
  );
};

export default VerifyCodeModal;
