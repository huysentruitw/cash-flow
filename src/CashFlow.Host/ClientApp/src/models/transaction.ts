import { FinancialYear } from './financial-year';

export class TransactionCode {
  codeName!: string;
  transactionId!: string;
  dateAssigned!: Date;
}

export class Transaction {
  id!: string;
  evidenceNumber!: number;
  accountId!: string;
  supplierId: string;
  dateCreated!: Date;
  dateModified: Date;
  amountInCents!: number;
  isInternalTransfer!: boolean;
  description!: string;
  comment: string;
  financialYear!: FinancialYear;
  codes!: TransactionCode[];
}
