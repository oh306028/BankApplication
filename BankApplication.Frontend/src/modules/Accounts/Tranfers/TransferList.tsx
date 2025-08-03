import { useEffect, useState } from "react";
import TransferService, { type Details } from "./TransferService";
import dayjs from "dayjs";
import styles from "../../../styles/TransferList.module.css";

interface TransferListProps {
  publicId: string;
  accountNumber: string;
  filter: "all" | "sent" | "received";
}

const TransferList: React.FC<TransferListProps> = ({
  publicId,
  accountNumber,
  filter,
}) => {
  const [transfers, setTransfers] = useState<Details[]>([]);
  const [isLoading, setIsLoading] = useState(true);

  useEffect(() => {
    const fetchData = async () => {
      if (!publicId) return;
      setIsLoading(true);
      try {
        let data: Details[] = [];
        switch (filter) {
          case "sent":
            data = await TransferService.getSentTransfers(publicId);
            break;
          case "received":
            data = await TransferService.getReceivedTransfers(publicId);
            break;
          default:
            data = await TransferService.getList(publicId);
            break;
        }
        setTransfers(data);
      } catch (error) {
        setTransfers([]);
      } finally {
        setIsLoading(false);
      }
    };

    fetchData();
  }, [publicId, filter]);

  if (isLoading) {
    return <p className={styles.loading}>Ładowanie historii...</p>;
  }

  if (transfers.length === 0) {
    return (
      <p className={styles.noTransfers}>Brak transakcji do wyświetlenia.</p>
    );
  }

  return (
    <ul className={styles.transferList}>
      {transfers.map((p) => (
        <li className={styles.transferItem}>
          <div className={styles.dateAndAmount}>
            <span className={styles.date}>
              {dayjs(p.transferDate).format("DD.MM.YYYY HH:mm")}
            </span>
            <span
              className={`${styles.amount} ${
                p.receiverNumber === accountNumber
                  ? styles.income
                  : styles.expense
              }`}
            >
              {p.receiverNumber === accountNumber ? "+" : "-"}
              {p.amount.toFixed(2)}
            </span>
          </div>
          <div className={styles.details}>
            <p className={styles.title}>{p.title}</p>
            <div className={styles.partyInfo}>
              <p className={styles.partyName}>
                {p.receiverNumber === accountNumber
                  ? `Od: ${p.sender}`
                  : `Do: ${p.receiver}`}
              </p>
              <span className={styles.partyNumber}>
                {p.receiverNumber === accountNumber
                  ? p.senderNumber
                  : p.receiverNumber}
              </span>
            </div>
          </div>
        </li>
      ))}
    </ul>
  );
};

export default TransferList;
