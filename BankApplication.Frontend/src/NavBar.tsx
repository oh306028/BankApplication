import { NavLink } from "react-router";
import navStyles from "./styles/NavBar.module.css";

function NavBar() {
  return (
    <>
      <nav className={navStyles.navbar}>
        <div className={navStyles.navbarBrand}>
          <NavLink
            to="/"
            className={({ isActive }) =>
              isActive ? navStyles.navLinksActiveLogo : navStyles.navLinks
            }
          >
            PocketBank
          </NavLink>
        </div>
        <div>
          <NavLink
            to="/accounts/join"
            className={({ isActive }) =>
              isActive ? navStyles.navLinksActive : navStyles.navLinks
            }
          >
            Dołącz
          </NavLink>

          <NavLink
            to="/accounts/register"
            className={({ isActive }) =>
              isActive ? navStyles.navLinksActive : navStyles.navLinks
            }
          >
            Załóż konto
          </NavLink>
          <NavLink
            to="/accounts/login"
            className={({ isActive }) =>
              isActive ? navStyles.navLinksActive : navStyles.navLinks
            }
          >
            Zaloguj się
          </NavLink>
        </div>
      </nav>
    </>
  );
}
export default NavBar;
