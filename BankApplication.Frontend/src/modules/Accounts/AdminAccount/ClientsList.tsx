import { useEffect, useState } from "react";
import AccountsService, { type ClientDetails } from "../AccountsService";
import styles from "../../../styles/AdminList.module.css";

const renderValue = (value: string | null | undefined) => {
  if (!value || value.trim() === "") {
    return <span className={styles.emptyValue}>-</span>;
  }
  return value;
};

function ClientsList() {
  const [data, setData] = useState<ClientDetails[]>([]);
  const [isLoading, setIsLoading] = useState(true);

  useEffect(() => {
    const fetchClients = async () => {
      setIsLoading(true);
      try {
        const result = await AccountsService.getClientList();
        setData(result);
      } catch (error) {
        console.error("Błąd podczas pobierania listy klientów:", error);
        setData([]);
      } finally {
        setIsLoading(false);
      }
    };
    fetchClients();
  }, []);

  if (isLoading) {
    return <p className={styles.placeholder}>Ładowanie danych...</p>;
  }

  if (!data || data.length === 0) {
    return <p className={styles.placeholder}>Brak klientów do wyświetlenia.</p>;
  }

  return (
    <div className={styles.listContainer}>
      <h2 className={styles.title}>Lista Klientów</h2>
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
            {data.map((client, index) => (
              <tr key={client.pesel || index}>
                <td data-label="Imię i nazwisko">
                  {renderValue(client.fullName)}
                </td>
                <td data-label="PESEL" className={styles.peselCell}>
                  {renderValue(client.pesel)}
                </td>
                <td data-label="Email" className={styles.emailCell}>
                  {renderValue(client.email)}
                </td>
                <td data-label="Telefon">{renderValue(client.phone)}</td>
                <td data-label="Narodowość">
                  {renderValue(client.nationality)}
                </td>
                <td data-label="Kraj">{renderValue(client.country)}</td>
                <td data-label="Miasto">{renderValue(client.city)}</td>
                <td data-label="Kod Pocztowy">
                  {renderValue(client.postalCode)}
                </td>
                <td data-label="Nr domu/mieszkania">
                  {renderValue(client.number)}
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
}

export default ClientsList;
