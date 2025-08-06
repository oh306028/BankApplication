import { useEffect, useState } from "react";
import AccountsService, {
  type Form,
  type KeyValuePair,
} from "../AccountsService.ts";
import styles from "../../../styles/Picker.module.css";
import Modal from "../../../modals/AlertModal.tsx";

function Picker({
  onAccountCreated,
  onCancelPicking,
  hasAccount,
  ownTypes,
}: {
  onAccountCreated: () => Promise<void>;
  onCancelPicking?: () => void;
  hasAccount?: boolean;
  ownTypes?: KeyValuePair[];
}) {
  const model: Form = {
    type: "",
    currency: "",
    interestRate: 1,
    credit: 0,
  };

  const [bankAccountTypes, setBankAccountTypes] = useState<KeyValuePair[]>([]);
  const [interestRates, setInterestRates] = useState<KeyValuePair[]>([]);
  const [currencies, setCurrencies] = useState<KeyValuePair[]>([]);
  const [creditAmounts, setCreditAmounts] = useState<KeyValuePair[]>([]);

  const [form, setForm] = useState<Form>(model);

  const [activeTile, setActiveTile] = useState<string | null>(null);
  const [selectedInterestRate, setSelectedInterestRate] = useState<
    number | null
  >(null);
  const [selectedCurrency, setSelectedCurrency] = useState<string | null>(null);
  const [selectedCredit, setSelectedCredit] = useState<number | null>(null);

  const [isModalOpen, setIsModalOpen] = useState(false);
  const [modalMessage, setModalMessage] = useState("");

  const showModal = (message: string) => {
    setModalMessage(message);
    setIsModalOpen(true);
  };

  useEffect(() => {
    onCreated();
  }, []);

  const userHasAccountType = (accountType: string): boolean => {
    const hasAccount =
      ownTypes?.some((ownType) => {
        return ownType.value === accountType;
      }) || false;
    return hasAccount;
  };

  const resetSelections = () => {
    setSelectedInterestRate(null);
    setSelectedCurrency(null);
    setSelectedCredit(null);
  };

  const handleOptionSelection = (
    tileType: string,
    setter: React.Dispatch<React.SetStateAction<any>>,
    value: any
  ) => {
    if (userHasAccountType(tileType)) {
      return;
    }

    if (tileType !== activeTile) {
      resetSelections();
      setActiveTile(tileType);
      setter(value);
    } else {
      setter((prev: any) => (prev === value ? null : value));
    }
  };

  const handleSelectAccount = async (accountType: string) => {
    if (userHasAccountType(accountType)) {
      return;
    }

    if (accountType === "Saving") {
      if (!selectedCurrency || !selectedInterestRate) {
        showModal(
          "Dla konta oszczędnościowego należy wybrać walutę i oprocentowanie."
        );
        return;
      }
    }

    if (accountType === "Credit") {
      if (!selectedCurrency || !selectedCredit) {
        showModal(
          "Dla konta kredytowego należy wybrać walutę i kwotę kredytu."
        );
        return;
      }
    }

    const finalForm: Form = {
      type: accountType,
      currency: selectedCurrency || "",
      interestRate: selectedInterestRate || 1,
      credit: selectedCredit || 0,
    };

    setForm(finalForm);
    try {
      await AccountsService.createBankAccount(finalForm);
      await onAccountCreated();
    } catch (ex: any) {
      showModal("Nie udało się utworzyć konta, błąd serwera.");
    }
  };

  const onCreated = async () => {
    const types = await AccountsService.getTypes();
    const interestRates = await AccountsService.getInterestRates();
    const currencies = await AccountsService.getCurrencies();
    const creditAmounts = await AccountsService.getCreditAmounts();
    setBankAccountTypes(types);
    setInterestRates(interestRates);
    setCurrencies(currencies);
    setCreditAmounts(creditAmounts);
  };

  function handleCancel(): void {
    if (onCancelPicking) {
      onCancelPicking();
    }
  }

  return (
    <div className={styles.pageWrapper}>
      <div className={styles.container}>
        <h2 className={styles.title}>
          {hasAccount
            ? "Dodaj nowy rachunek"
            : "Utwórz swój pierwszy rachunek bankowy!"}
        </h2>
        <div className={styles.decorativeLine}></div>

        <div className={styles.tileRow}>
          {bankAccountTypes.map((type) => {
            const isOwned = userHasAccountType(type.value);
            return (
              <div
                key={type.key}
                className={`${styles.tile} ${
                  activeTile === type.value ? styles.activeTile : ""
                } ${isOwned ? styles.disabledTile : ""}`}
              >
                <h3 className={styles.tileTitle}>
                  {type.name}
                  {isOwned && (
                    <span className={styles.ownedLabel}> (Posiadasz)</span>
                  )}
                </h3>
                <div className={styles.details}>
                  {type.value === "Saving" && (
                    <>
                      <p className={styles.subtitle}>Wybierz oprocentowanie</p>
                      <ul className={styles.list}>
                        {interestRates.map((rate) => (
                          <li
                            key={rate.key}
                            onClick={() =>
                              handleOptionSelection(
                                type.value,
                                setSelectedInterestRate,
                                rate.key
                              )
                            }
                            className={`${styles.listItem} ${
                              activeTile === type.value &&
                              selectedInterestRate === rate.key
                                ? styles.selected
                                : ""
                            } ${isOwned ? styles.disabledListItem : ""}`}
                          >
                            {rate.name}
                          </li>
                        ))}
                      </ul>
                    </>
                  )}

                  {(type.value === "Saving" || type.value === "Credit") && (
                    <>
                      <p className={styles.subtitle}>Wybierz walutę</p>
                      <ul className={styles.list}>
                        {currencies.map((currency) => (
                          <li
                            key={currency.key}
                            onClick={() =>
                              handleOptionSelection(
                                type.value,
                                setSelectedCurrency,
                                currency.name
                              )
                            }
                            className={`${styles.listItem} ${
                              activeTile === type.value &&
                              selectedCurrency === currency.name
                                ? styles.selected
                                : ""
                            } ${isOwned ? styles.disabledListItem : ""}`}
                          >
                            {currency.name}
                          </li>
                        ))}
                      </ul>
                    </>
                  )}

                  {type.value === "Credit" && (
                    <>
                      <p className={styles.subtitle}>Wybierz kwotę</p>
                      <ul className={styles.list}>
                        {creditAmounts.map((credit) => (
                          <li
                            key={credit.key}
                            onClick={() =>
                              handleOptionSelection(
                                type.value,
                                setSelectedCredit,
                                credit.key
                              )
                            }
                            className={`${styles.listItem} ${
                              activeTile === type.value &&
                              selectedCredit === credit.key
                                ? styles.selected
                                : ""
                            } ${isOwned ? styles.disabledListItem : ""}`}
                          >
                            {credit.name}
                          </li>
                        ))}
                      </ul>
                    </>
                  )}

                  {!isOwned && (
                    <button
                      onClick={() => handleSelectAccount(type.value)}
                      className={styles.submitButton}
                    >
                      Wybierz
                    </button>
                  )}
                </div>
              </div>
            );
          })}
        </div>
        <button onClick={() => handleCancel()} className={styles.cancelButton}>
          Anuluj
        </button>
      </div>

      <Modal isOpen={isModalOpen} onClose={() => setIsModalOpen(false)}>
        <h3 style={{ marginBottom: "15px" }}>Uwaga</h3>
        <p>{modalMessage}</p>
      </Modal>
    </div>
  );
}

export default Picker;
