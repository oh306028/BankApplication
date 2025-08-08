import styles from "../../.././styles/AcceptationAwait.module.css";
import NavBar from "../../../NavBar";
import Footer from "../../../Footer";
import { useNavigate } from "react-router";

function AcceptationAwait() {
  const navigate = useNavigate();

  return (
    <>
      <NavBar />
      <div className={styles.awaitContainer}>
        <span className={styles.icon}>🕒</span>
        <h2 className={styles.mainTitle}>
          Dziękujemy za złożenie wniosku o zostanie klientem PocketBank
        </h2>
        <div className={styles.detailsText}>
          <p>
            Potwierdzenie o akceptacji zostanie przesłane na Twój adres e-mail.
          </p>
          <p>
            W wiadomości znajdziesz swój <strong>unikalny kod klienta</strong>,
            który będzie niezbędny do założenia konta.
          </p>
          <p>
            Po otrzymaniu akceptacji, prosimy o dokończenie procesu rejestracji.
          </p>
        </div>
        <button onClick={() => navigate("/")} className={styles.homeButton}>
          Powrót na stronę główną
        </button>
      </div>
      <Footer />
    </>
  );
}

export default AcceptationAwait;
