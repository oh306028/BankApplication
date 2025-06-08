import { useState } from "react";
import styles from "../../.././styles/Login.module.css";
import { useNavigate } from "react-router";
import AuthenticationService, {
  type RegisterModel,
} from "../AuthenticationService";
import NavBar from "../../../NavBar";
import Footer from "../../../Footer";

function Register() {
  const [formData, setFormData] = useState<RegisterModel>({
    email: "",
    login: "",
    password: "",
    confirmPassword: "",
    clientCode: "",
  });

  const handleChange = (e) => {
    const { name, value } = e.target;

    setFormData((prev) => ({
      ...prev,
      [name]: value,
    }));

    setErrors((prevErrors) => {
      const newErrors = { ...prevErrors };
      const capitalizedName = name.charAt(0).toUpperCase() + name.slice(1);
      delete newErrors[capitalizedName];
      return newErrors;
    });
  };

  const navigate = useNavigate();

  type ApiErrorResponse = {
    [key: string]: string[];
  };

  const [errors, setErrors] = useState<ApiErrorResponse>({});

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setErrors({});

    try {
      await AuthenticationService.register(formData);
      navigate("/bankAccounts");
    } catch (error: any) {
      if (error.response.status === 422) {
        setErrors(error.response.data);
      }
      if (error.response.status === 404)
        setErrors({ ["NotFound"]: [error.response.data] });

      if (error.response.status === 400)
        alert("Konto klienta nie jest aktywne!");
    }
  };

  return (
    <>
      <NavBar />

      <div className={styles.loginContainer}>
        <h2 className={styles.loginTitle}>Rejestracja konta PocketBank</h2>
        <div className={styles.decorativeLine}></div>

        <form onSubmit={handleSubmit}>
          <div className={styles.formGroup}>
            <label htmlFor="clientCode" className={styles.label}>
              Kod klienta PocketBank
            </label>
            <input
              type="text"
              id="clientCode"
              name="clientCode"
              className={styles.input}
              value={formData.clientCode}
              onChange={handleChange}
              placeholder="wpisz swój unikalny kod"
            />
            {errors.ClientCode && errors.ClientCode.length > 0 && (
              <p className={styles.error}>{errors.ClientCode[0]}</p>
            )}
          </div>
          <div className={styles.formGroup}>
            <label htmlFor="email" className={styles.label}>
              Email
            </label>
            <input
              type="email"
              id="email"
              name="email"
              className={styles.input}
              value={formData.email}
              onChange={handleChange}
              placeholder="wpisz swój email"
            />
            {errors.Email && errors.Email.length > 0 && (
              <p className={styles.error}>{errors.Email[0]}</p>
            )}
          </div>

          <div className={styles.formGroup}>
            <label htmlFor="login" className={styles.label}>
              Login
            </label>
            <input
              type="text"
              id="login"
              name="login"
              className={styles.input}
              value={formData.login}
              onChange={handleChange}
              placeholder="wpisz swój login"
            />
            {errors.Login && errors.Login.length > 0 && (
              <p className={styles.error}>{errors.Login[0]}</p>
            )}
          </div>
          <div className={styles.doubleForm}>
            <div className={styles.formGroup}>
              <label htmlFor="password" className={styles.label}>
                Hasło
              </label>
              <input
                type="password"
                id="password"
                name="password"
                className={styles.input}
                value={formData.password}
                onChange={handleChange}
                placeholder="wpisz swoje hasło"
              />
              {errors.Password && errors.Password.length > 0 && (
                <p className={styles.error}>{errors.Password[0]}</p>
              )}
            </div>

            <div className={styles.formGroup}>
              <label htmlFor="confirmPassword" className={styles.label}>
                Powtórz hasło
              </label>
              <input
                type="password"
                id="confirmPassword"
                name="confirmPassword"
                className={styles.input}
                value={formData.confirmPassword}
                onChange={handleChange}
                placeholder="Powtórz swoje hasło"
              />
              {errors.ConfirmPassword && errors.ConfirmPassword.length > 0 && (
                <p className={styles.error}>{errors.ConfirmPassword[0]}</p>
              )}
            </div>
          </div>
          <button type="submit" className={styles.submitButton}>
            Rejestruj
          </button>
        </form>
        {errors.NotFound && errors.NotFound.length > 0 && (
          <p className={styles.errorFound}>{errors.NotFound[0]}</p>
        )}
      </div>

      <Footer />
    </>
  );
}

export default Register;
