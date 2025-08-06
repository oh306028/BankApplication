import { useEffect, useState } from "react";
import AccountsService, { type ClientDetails } from "../AccountsService";
import styles from "../../../styles/AdminList.module.css";

const renderValue = (value: string | null | undefined) => {
  if (!value || value.trim() === "") {
    return <span className={styles.emptyValue}>-</span>;
  }
  return value;
};

function AdminList() {
  const [data, setData] = useState<ClientDetails[]>([]);
  const [isLoading, setIsLoading] = useState(true);

  useEffect(() => {
    const fetchAdmins = async () => {
      setIsLoading(true);
      try {
        const result = await AccountsService.getAdminList();
        setData(result);
      } catch (error) {
        console.error("Błąd podczas pobierania listy administratorów:", error);
        setData([]);
      } finally {
        setIsLoading(false);
      }
    };
    fetchAdmins();
  }, []);

  if (isLoading) {
    return <p className={styles.placeholder}>Ładowanie danych...</p>;
  }

  if (!data || data.length === 0) {
    return (
      <p className={styles.placeholder}>
        Brak administratorów do wyświetlenia.
      </p>
    );
  }

  return (
    <div className={styles.listContainer}>
      <h2 className={styles.title}>Lista Administratorów</h2>
      <div className={styles.tableWrapper}>
        <table className={styles.adminsTable}>
          <thead>
            <tr>
              <th>Imię i nazwisko</th>
              <th>PESEL</th>
              <th>Email</th>
              <th>Telefon</th>
              <th>Narodowość</th>
              <th>Kraj</th>
              <th>Miasto</th>
              <th>Kod Pocztowy</th>
              <th>Nr domu/mieszkania</th>
            </tr>
          </thead>
          <tbody>
            {data.map((p, index) => (
              <tr key={p.pesel || index}>
                <td data-label="Imię i nazwisko">{renderValue(p.fullName)}</td>
                <td data-label="PESEL" className={styles.peselCell}>
                  {renderValue(p.pesel)}
                </td>
                <td data-label="Email" className={styles.emailCell}>
                  {renderValue(p.email)}
                </td>
                <td data-label="Telefon">{renderValue(p.phone)}</td>
                <td data-label="Narodowość">{renderValue(p.nationality)}</td>
                <td data-label="Kraj">{renderValue(p.country)}</td>
                <td data-label="Miasto">{renderValue(p.city)}</td>
                <td data-label="Kod Pocztowy">{renderValue(p.postalCode)}</td>
                <td data-label="Nr domu/mieszkania">{renderValue(p.number)}</td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
}

export default AdminList;
