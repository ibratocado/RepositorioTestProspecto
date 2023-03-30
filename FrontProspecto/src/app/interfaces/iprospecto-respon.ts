import { IstatusRespon } from "./istatus-respon"

export interface IprospectoRespon {
  status: IstatusRespon
  id: string
  nombre: string
  primerApellido: string
  segundoApellido: string
  calle: string
  numero: number
  colonia: string
  codigoPostal: string
  telefono: string
  rfc: string
}

