import api from './index'
import { axios } from '@/utils/request'

export function login (parameter) {
  return axios({
    url: '/Identity/login',
    method: 'post',
    data: parameter
  })
}
