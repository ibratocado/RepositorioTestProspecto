export interface IgenericRespon<T>{
  respon: GenericData<T>;
}

export interface GenericData<T> {
  State: number;
  Messaget: string;
  Data: T;
}
