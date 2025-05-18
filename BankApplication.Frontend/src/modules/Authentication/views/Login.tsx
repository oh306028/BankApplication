import { useState } from "react";
import styles from "../../.././styles/Login.module.css";
import { useNavigate } from "react-router";
import AuthenticationService from "../AuthenticationService";
import NavBar from "../../../NavBar";
import Footer from "../../../Footer";

function Login() {
  const [formData, setFormData] = useState({
    email: "",
    login: "",
    password: "",
  });

  const handleChange = (e) => {
    setFormData((prev) => ({
      ...prev,
      [e.target.name]: e.target.value,
    }));
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
      const response = await AuthenticationService.login(formData);
      navigate("/bankAccounts");
    } catch (error: any) {
      if (error.response.status === 422) {
        setErrors(error.response.data);
      }
      if (error.response.status === 404)
        setErrors({ ["NotFound"]: [error.response.data] });
    }
  };

  return (
    <>
      <NavBar />

      <div className={styles.loginContainer}>
        <h2 className={styles.loginTitle}>Logowanie do PocketBank</h2>
        <div className={styles.decorativeLine}></div>

        <form onSubmit={handleSubmit}>
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
          <button type="submit" className={styles.submitButton}>
            Zaloguj się
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

export default Login;
