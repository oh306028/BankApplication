import { useEffect, useState } from "react";
import AccountsService, { type ProfileDetails } from "../AccountsService";
import dayjs from "dayjs";
import styles from "../../../styles/Profile.module.css";
import { useNavigate } from "react-router";
import ClientNavBar from "./ClientNavBar";
import Footer from "../../../Footer";

function Profile() {
  const [data, setData] = useState<ProfileDetails | null>(null);
  const [isLoading, setIsLoading] = useState(true);
  const navigate = useNavigate();

  useEffect(() => {
    const fetchData = async () => {
      setIsLoading(true);
      try {
        const result = await AccountsService.getDetails();
        setData(result);
      } catch (error) {
        console.error("Błąd podczas pobierania danych profilu:", error);
        setData(null);
      } finally {
        setIsLoading(false);
      }
    };
    fetchData();
  }, []);

  const renderInfoRow = (label: string, value: React.ReactNode) => (
    <div className={styles.infoRow}>
      <span className={styles.label}>{label}</span>
      <span className={styles.value}>{value || "-"}</span>
    </div>
  );

  return (
    <>
      <ClientNavBar />
      <div className={styles.profilePage}>
        <header className={styles.header}>
          <h1 className={styles.title}>Profil Użytkownika</h1>
          <button
            onClick={() => navigate("/bankaccounts")}
            className={styles.backButton}
          >
            Powrót do kont
          </button>
        </header>

        {isLoading ? (
          <p className={styles.placeholder}>Ładowanie danych...</p>
        ) : data ? (
          <div className={styles.profileGrid}>
            <div className={styles.profileCard}>
              <h2 className={styles.cardTitle}>Dane Logowania</h2>
              {renderInfoRow("Login", data.login)}
              {renderInfoRow(
                "Data utworzenia",
                dayjs(data.createdDate).format("DD.MM.YYYY")
              )}
              {renderInfoRow(
                "Weryfikacja 2FA",
                <span
                  className={
                    data.isDoubleAuthenticated
                      ? styles.statusActive
                      : styles.statusInactive
                  }
                >
                  {data.isDoubleAuthenticated ? "Aktywna" : "Nieaktywna"}
                </span>
              )}
            </div>

            <div className={styles.profileCard}>
              <h2 className={styles.cardTitle}>Dane Osobowe</h2>
              {renderInfoRow("Imię", data.firstName)}
              {renderInfoRow("Nazwisko", data.lastName)}
              {renderInfoRow("PESEL", data.pesel)}
              {renderInfoRow(
                "Data urodzenia",
                dayjs(data.birthDate).format("DD.MM.YYYY")
              )}
            </div>

            <div className={styles.profileCard}>
              <h2 className={styles.cardTitle}>Dane Kontaktowe</h2>
              {renderInfoRow("Adres e-mail", data.email)}
              {renderInfoRow("Numer telefonu", data.phone)}
            </div>

            <div className={styles.profileCard}>
              <h2 className={styles.cardTitle}>Adres</h2>
              {renderInfoRow("Kraj", data.country)}
              {renderInfoRow("Miasto", data.city)}
              {renderInfoRow("Kod pocztowy", data.postalCode)}
              {renderInfoRow("Numer domu/mieszkania", data.number)}
            </div>
          </div>
        ) : (
          <p className={styles.placeholder}>
            Nie udało się załadować danych profilu.
          </p>
        )}
      </div>
      <Footer />
    </>
  );
}

export default Profile;
