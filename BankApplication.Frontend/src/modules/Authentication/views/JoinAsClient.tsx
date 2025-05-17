import navStyles from "../../../styles/WelcomePage.module.css";
import { NavLink } from "react-router";
function JoinAsClient() {

  return (
    <>
<nav className={navStyles.navbar}>
        <div className={navStyles.navbarBrand}>PocketBank</div>
        <div>
          <NavLink to="/accounts/join" className={navStyles.navLinks}>Dołącz</NavLink>
          <NavLink to="/accounts/login" className={navStyles.navLinks}>Zaloguj się</NavLink>
          <NavLink to="/accounts/register" className={navStyles.navLinks}>Rejestracja</NavLink>
        </div>
      </nav>

      <h1>Dolacz do nas</h1>
       <footer className={navStyles.footer}>
        <p>© 2025 PocketBank. Wszelkie prawa zastrzeżone.</p>
        <p>Kontakt: kontakt@pocketbank.pl | Telefon: +48 123 456 789</p>
      </footer>
    </>
  )
}

export default JoinAsClient
