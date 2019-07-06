import { FinancialYear } from './financial-year';
import { Supplier } from './supplier';

export class TransactionCode {
  codeName!: string;
  transactionId!: string;
  dateAssigned!: Date;
}

export class Transaction {
  id!: string;
  evidenceNumber!: number;
  accountId!: string;
  transactionDate: Date;
  amountInCents!: number;
  isInternalTransfer!: boolean;
  description!: string;
  comment: string;
  codes!: TransactionCode[];
  financialYear: FinancialYear;
  supplier: Supplier;
}
