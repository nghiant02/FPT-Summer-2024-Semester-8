export interface BuyBackCountResponse{
   message: MessageCount
}

export interface MessageCount {
  totalPrice: number
}
export interface BuyBackResponse{
    message: Message
}
export interface Message {
    totalPrice: number
    billId: string
  }