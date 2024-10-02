import axios from 'axios';

export const registerWorker = (data) => {
  return axios.post('http://localhost:5001/api/User/registerRab', data);
};

export const registerBoss = (data) => {
  return axios.post('http://localhost:5001/api/User/registerBoss', data);
};
