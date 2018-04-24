import Cookies from 'js-cookie'
import request from '@/utils/request'

const TokenKey = 'Cookies';

export function getToken() {
    return request({
        url: '/account/info',
        method: 'get' //,
        //params: { token }
    });
}

export function setToken(token) {
  return Cookies.set(TokenKey, token);
}

export function removeToken() {
  return Cookies.remove(TokenKey);
}
