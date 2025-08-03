import axios from "axios";
export default class AccountsService {
  public static async hasBankAccounts(): Promise<boolean> {
    return (await axios.get<boolean>(`bank-accounts`)).data;
  }

  public static async getTypes(): Promise<KeyValuePair[]> {
    return (await axios.get<KeyValuePair[]>(`bank-accounts/types`)).data;
  }

  public static async getOwnTypes(): Promise<KeyValuePair[]> {
    return (await axios.get<KeyValuePair[]>(`bank-accounts/own-types`)).data;
  }

  public static async getDetailsByType(type: string): Promise<Details> {
    return (await axios.get<Details>(`bank-accounts/${type}`)).data;
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

  public static async getClientList(): Promise<ClientDetails[]> {
    return (await axios.get<ClientDetails[]>(`clients`)).data;
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

export interface Details {
  accountNumber: string;
  balance: number;
  currency: string;
  interestRate?: number;
  publicId: string;
}

export interface ClientDetails {
  fullName: string;
  email: string;
  phone: string;
  nationality: string;
  country: string;
  city: string;
  postalCode: string;
  number: string;
  pesel: string;
}
