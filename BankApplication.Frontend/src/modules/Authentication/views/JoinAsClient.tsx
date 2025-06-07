import NavBar from "../../../NavBar";
import styles from "../../.././styles/JoinAsClient.module.css";
import { useState } from "react";
import { useNavigate } from "react-router";
import AuthenticationService, {
  type ClientForm,
} from "../AuthenticationService";

function JoinAsClient() {
  const [formData, setFormData] = useState<ClientForm>({
    firstName: "",
    lastName: "",
    email: "",
    phone: "",
    pesel: "",
    birthDate: null,
    nationality: "",
    country: "",
    postalCode: "",
    number: "",
    city: "",
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
      const response = await AuthenticationService.join(formData);
      navigate("/bankAccounts"); //navigate to the waiting page within the status
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
      <div className={styles.joinAsClientContainer}>
        <h2 className={styles.title}>Zostań klientem PocketBank!</h2>
        <div className={styles.decorativeLine}></div>

        <form onSubmit={handleSubmit}>
          <div className={styles.doubleForm}>
            <div className={styles.formGroup}>
              <label htmlFor="firstName" className={styles.label}>
                Imię
              </label>
              <input
                type="firstName"
                id="firstName"
                name="firstName"
                className={styles.input}
                value={formData.firstName}
                onChange={handleChange}
                placeholder="wpisz swoje imię"
              />
              {errors.FirstName && errors.FirstName.length > 0 && (
                <p className={styles.error}>{errors.FirstName[0]}</p>
              )}
            </div>

            <div className={styles.formGroup}>
              <label htmlFor="lastName" className={styles.label}>
                Nazwisko
              </label>
              <input
                type="text"
                id="lastName"
                name="lastName"
                className={styles.input}
                value={formData.lastName}
                onChange={handleChange}
                placeholder="wpisz swoje nazwisko"
              />
              {errors.LastName && errors.LastName.length > 0 && (
                <p className={styles.error}>{errors.LastName[0]}</p>
              )}
            </div>
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
            {errors.NotFound && errors.NotFound.length > 0 && (
              <p className={styles.errorFound}>{errors.NotFound[0]}</p>
            )}
          </div>

          <div className={styles.formGroup}>
            <label htmlFor="pesel" className={styles.label}>
              Pesel
            </label>
            <input
              type="text"
              id="pesel"
              name="pesel"
              className={styles.input}
              value={formData.pesel}
              onChange={handleChange}
              placeholder="wpisz swój pesel"
            />
            {errors.Pesel && errors.Pesel.length > 0 && (
              <p className={styles.error}>{errors.Pesel[0]}</p>
            )}
          </div>
          <div className={styles.doubleForm}>
            <div className={styles.formGroup}>
              <label htmlFor="birthDate" className={styles.label}>
                Data urodzenia
              </label>
              <input
                type="date"
                id="birthDate"
                name="birthDate"
                className={styles.input}
                value={formData.birthDate}
                onChange={handleChange}
              />
              {errors.BirthDate && errors.BirthDate.length > 0 && (
                <p className={styles.error}>{errors.BirthDate[0]}</p>
              )}
            </div>

            <div className={styles.formGroup}>
              <label htmlFor="phone" className={styles.label}>
                Numer telefonu
              </label>
              <input
                type="text"
                id="phone"
                name="phone"
                className={styles.input}
                value={formData.phone}
                onChange={handleChange}
                placeholder="wpisz swój numer telefonu"
              />
              {errors.Phone && errors.Phone.length > 0 && (
                <p className={styles.error}>{errors.Phone[0]}</p>
              )}
            </div>
          </div>

          <div className={styles.formGroup}>
            <label htmlFor="nationality" className={styles.label}>
              Narodowość
            </label>
            <input
              type="text"
              id="nationality"
              name="nationality"
              className={styles.input}
              value={formData.nationality}
              onChange={handleChange}
              placeholder="wpisz swoją narodowość"
            />
            {errors.Nationality && errors.Nationality.length > 0 && (
              <p className={styles.error}>{errors.Nationality[0]}</p>
            )}
          </div>

          <div className={styles.formGroup}>
            <label htmlFor="country" className={styles.label}>
              Państwo
            </label>
            <input
              type="text"
              id="country"
              name="country"
              className={styles.input}
              value={formData.country}
              onChange={handleChange}
              placeholder="wpisz swój kraj pochenia"
            />
            {errors.Country && errors.Country.length > 0 && (
              <p className={styles.error}>{errors.Country[0]}</p>
            )}
          </div>
          <div className={styles.doubleForm}>
            <div className={styles.formGroup}>
              <label htmlFor="city" className={styles.label}>
                Miasto
              </label>
              <input
                type="text"
                id="city"
                name="city"
                className={styles.input}
                value={formData.city}
                onChange={handleChange}
                placeholder="wpisz swoje miasto"
              />
              {errors.City && errors.City.length > 0 && (
                <p className={styles.error}>{errors.City[0]}</p>
              )}
            </div>

            <div className={styles.formGroup}>
              <label htmlFor="postalCode" className={styles.label}>
                Kod pocztowy
              </label>
              <input
                type="text"
                id="postalCode"
                name="postalCode"
                className={styles.input}
                value={formData.postalCode}
                onChange={handleChange}
                placeholder="wpisz swój kod pocztowy"
              />
              {errors.PostalCode && errors.PostalCode.length > 0 && (
                <p className={styles.error}>{errors.PostalCode[0]}</p>
              )}
            </div>
          </div>

          <div className={styles.formGroup}>
            <label htmlFor="number" className={styles.label}>
              Numer mieszkania / domu
            </label>
            <input
              type="text"
              id="number"
              name="number"
              className={styles.input}
              value={formData.number}
              onChange={handleChange}
              placeholder="wpisz swój numer domu"
            />
            {errors.Number && errors.Number.length > 0 && (
              <p className={styles.error}>{errors.Number[0]}</p>
            )}
          </div>

          <button type="submit" className={styles.submitButton}>
            Dołącz do nas
          </button>
        </form>
      </div>
    </>
  );
}

export default JoinAsClient;
