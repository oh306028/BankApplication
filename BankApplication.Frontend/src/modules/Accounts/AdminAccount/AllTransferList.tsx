import { useEffect, useState } from "react";
import TransferService, { type Details } from "../Tranfers/TransferService";
import dayjs from "dayjs";
import styles from "../../../styles/AllTransfersList.module.css";

function AllTransferList() {
  const [data, setData] = useState<Details[]>([]);
  const [isLoading, setIsLoading] = useState(true);

  useEffect(() => {
    const fetchData = async () => {
      setIsLoading(true);
      try {
        const result = await TransferService.getAll();
        setData(result);
      } catch (error) {
        console.error("Błąd podczas pobierania wszystkich transferów:", error);
        setData([]);
      } finally {
        setIsLoading(false);
      }
    };
    fetchData();
  }, []);

  if (isLoading) {
    return <p className={styles.placeholder}>Ładowanie danych...</p>;
  }

  if (!data || data.length === 0) {
    return <p className={styles.placeholder}>Brak transferów w systemie.</p>;
  }

  return (
    <div className={styles.listContainer}>
      <h2 className={styles.title}>Transfery w Systemie</h2>
      <div className={styles.scrollWrapper}>
        <ul className={styles.transferList}>
          {data.map((p) => (
            <li className={styles.transferItem}>
              <div className={styles.header}>
                <span className={styles.date}>
                  {dayjs(p.transferDate).format("DD.MM.YYYY HH:mm")}
                </span>
                <span className={styles.amount}>{p.amount.toFixed(2)}</span>
              </div>
              <div className={styles.mainInfo}>
                <p className={styles.transferTitle}>{p.title}</p>
                <div className={styles.partiesContainer}>
                  <div className={styles.partyBlock}>
                    <span className={styles.partyLabel}>Nadawca</span>
                    <p className={styles.partyName}>{p.sender}</p>
                    <p className={styles.partyNumber}>{p.senderNumber}</p>
                  </div>
                  <span className={styles.arrowIcon}>→</span>
                  <div className={styles.partyBlock}>
                    <span className={styles.partyLabel}>Odbiorca</span>
                    <p className={styles.partyName}>{p.receiver}</p>
                    <p className={styles.partyNumber}>{p.receiverNumber}</p>
                  </div>
                </div>
              </div>
            </li>
          ))}
        </ul>
      </div>
    </div>
  );
}

export default AllTransferList;
