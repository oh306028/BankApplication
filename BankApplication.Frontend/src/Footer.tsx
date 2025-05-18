import styles from "./styles/Footer.module.css";

function Footer() {
  return (
    <>
      <footer className={styles.footer}>
        <p>© 2025 PocketBank. Wszelkie prawa zastrzeżone.</p>
        <p>Kontakt: kontakt@pocketbank.pl | Telefon: +48 123 456 789</p>
      </footer>
    </>
  );
}
export default Footer;
