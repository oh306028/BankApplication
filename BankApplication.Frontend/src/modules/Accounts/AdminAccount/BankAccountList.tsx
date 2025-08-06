import { useEffect, useState } from "react";
import AccountsService, { type Details } from "../AccountsService";
import styles from "../../../styles/BankAccountList.module.css";

const renderOptionalValue = (
  value: number | string | null | undefined,
  suffix = ""
) => {
  if (value === null || value === undefined || value === "") {
    return <span className={styles.emptyValue}>-</span>;
  }
  if (value === 1) return <span className={styles.emptyValue}>-</span>;

  return `${value}${suffix}`;
};

function BankAccountList() {
  const [data, setData] = useState<Details[]>([]);
  const [isLoading, setIsLoading] = useState(true);

  useEffect(() => {
    const fetchAccounts = async () => {
      setIsLoading(true);
      try {
        const result = await AccountsService.getBankAccounts();
        setData(result);
      } catch (error) {
        console.error("Błąd podczas pobierania listy kont bankowych:", error);
        setData([]);
      } finally {
        setIsLoading(false);
      }
    };
    fetchAccounts();
  }, []);

  if (isLoading) {
    return <p className={styles.placeholder}>Ładowanie danych...</p>;
  }

  if (!data || data.length === 0) {
    return (
      <p className={styles.placeholder}>Brak kont bankowych do wyświetlenia.</p>
    );
  }

  return (
    <div className={styles.listContainer}>
      <h2 className={styles.title}>Lista Kont Bankowych</h2>
      <div className={styles.tableWrapper}>
        <table className={styles.accountsTable}>
          <thead>
            <tr>
              <th>Numer Konta</th>
              <th>Saldo</th>
              <th>Oprocentowanie</th>
              <th>ID</th>
            </tr>
          </thead>
          <tbody>
            {data.map((account) => (
              <tr key={account.publicId}>
                <td
                  data-label="Numer Konta"
                  className={styles.accountNumberCell}
                >
                  {account.accountNumber}
                </td>
                <td data-label="Saldo" className={styles.balanceCell}>
                  {account.balance.toFixed(2)}
                  <span className={styles.currency}> {account.currency}</span>
                </td>
                <td data-label="Oprocentowanie">
                  {renderOptionalValue(account.interestRate, "%")}
                </td>
                <td data-label="Publiczny ID" className={styles.publicIdCell}>
                  {account.publicId}
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
}

export default BankAccountList;
