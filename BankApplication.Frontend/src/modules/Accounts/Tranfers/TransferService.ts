import axios from "axios";
export default class TransferService {
  public static async send(accountId: string, model: Form): Promise<void> {
    await axios.post<Form>(`bank-accounts/${accountId}/transfers`, model);
  }

  public static async getList(accountId: string): Promise<Details[]> {
    return (await axios.get<Details[]>(`bank-accounts/${accountId}/transfers`))
      .data;
  }

  public static async getAll(): Promise<Details[]> {
    return (await axios.get<Details[]>(`bank-accounts/dictionary/transfers`))
      .data;
  }

  public static async getSentTransfers(accountId: string): Promise<Details[]> {
    return (
      await axios.get<Details[]>(`bank-accounts/${accountId}/transfers/sent`)
    ).data;
  }

  public static async downloadTransfersPdf(accountId: string): Promise<void> {
    try {
      const response = await axios.get(
        `bank-accounts/${accountId}/transfers/download`,
        {
          responseType: "blob",
          headers: {
            Accept: "application/pdf",
          },
        }
      );

      const blob = new Blob([response.data], { type: "application/pdf" });
      const url = window.URL.createObjectURL(blob);

      const link = document.createElement("a");
      link.href = url;
      link.download = `HistoriaPrzelewow-${
        new Date().toISOString().split("T")[0]
      }.pdf`;

      document.body.appendChild(link);
      link.click();
      document.body.removeChild(link);

      window.URL.revokeObjectURL(url);
    } catch (error) {
      console.error("Błąd podczas pobierania PDF:", error);
      throw error;
    }
  }

  public static async getReceivedTransfers(
    accountId: string
  ): Promise<Details[]> {
    return (
      await axios.get<Details[]>(
        `bank-accounts/${accountId}/transfers/received`
      )
    ).data;
  }
}

export interface Form {
  accountToNumber: string;
  amount: number;
  title: string;
  description: string;
}

export interface Details {
  senderNumber: string;
  receiverNumber: string;
  sender: string;
  receiver: string;
  amount: number;
  title: string;
  descrition: string;
  transferDate: string;
}
