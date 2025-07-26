import axios from "axios";
export default class AccountsService {
  public static async hasBankAccounts(): Promise<boolean> {
    return (await axios.get<boolean>(`bank-accounts`)).data;
  }

  public static async getTypes(): Promise<KeyValuePair[]> {
    return (await axios.get<KeyValuePair[]>(`bank-accounts/types`)).data;
  }

  public static async getInterestRates(): Promise<KeyValuePair[]> {
    return (await axios.get<KeyValuePair[]>(`bank-accounts/rates`)).data;
  }

  public static async getCurrencies(): Promise<KeyValuePair[]> {
    return (await axios.get<KeyValuePair[]>(`bank-accounts/currencies`)).data;
  }

  public static async getCreditAmounts(): Promise<KeyValuePair[]> {
    return (await axios.get<KeyValuePair[]>(`bank-accounts/credits`)).data;
  }

  public static async createBankAccount(model: Form): Promise<void> {
    await axios.post<KeyValuePair[]>(`bank-accounts`, model);
  }
}

export interface KeyValuePair {
  key: number;
  value: string;
  name: string;
}

export interface Form {
  type: number | string | KeyValuePair;
  currency: string;
  interestRate?: number;
  credit?: number;
}
