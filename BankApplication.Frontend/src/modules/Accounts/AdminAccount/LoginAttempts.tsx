import { useEffect, useState } from "react";
import AccountsService, { type LoginAttemptDetails } from "../AccountsService";
import dayjs from "dayjs";
import styles from "../../../styles/LoginAttempts.module.css";

function LoginAttempts() {
  const [data, setData] = useState<LoginAttemptDetails[]>([]);
  const [isLoading, setIsLoading] = useState(true);

  useEffect(() => {
    const fetchAttempts = async () => {
      setIsLoading(true);
      try {
        const result = await AccountsService.getLoginAttempts();
        const sortedData = result.sort(
          (a, b) =>
            new Date(b.logInDate).getTime() - new Date(a.logInDate).getTime()
        );
        setData(sortedData);
      } catch (error) {
        console.error("Błąd podczas pobierania prób logowania:", error);
        setData([]);
      } finally {
        setIsLoading(false);
      }
    };
    fetchAttempts();
  }, []);

  if (isLoading) {
    return <p className={styles.placeholder}>Ładowanie danych...</p>;
  }

  if (!data || data.length === 0) {
    return (
      <p className={styles.placeholder}>Brak prób logowania do wyświetlenia.</p>
    );
  }

  return (
    <div className={styles.listContainer}>
      <h2 className={styles.title}>Rejestr Prób Logowania</h2>
      <div className={styles.scrollWrapper}>
        <ul className={styles.logList}>
          {data.map((attempt, index) => (
            <li key={index} className={styles.logItem}>
              <span
                className={`${styles.statusIcon} ${
                  attempt.isSuccess ? styles.success : styles.failure
                }`}
              >
                {attempt.isSuccess ? "✔" : "✖"}
              </span>
              <span className={styles.clientName}>{attempt.clientName}</span>
              <span className={styles.logDate}>
                {dayjs(attempt.logInDate).format("DD.MM.YYYY HH:mm:ss")}
              </span>
            </li>
          ))}
        </ul>
      </div>
    </div>
  );
}

export default LoginAttempts;
