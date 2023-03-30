export interface IgenericRespon<T>{
  respon: GenericData<T>;
}

export interface GenericData<T> {
  state: number;
  message: string;
  data: T;
}
