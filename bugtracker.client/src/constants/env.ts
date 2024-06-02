const APIURL = import.meta.env.VITE_API_URL;

export const GETUSER = `${APIURL}/users`;
export const CREATEUSER = `${APIURL}/users/register`;
export const AUTHUSER = `${APIURL}/users/login`;
export const UPDATEUSER = `${APIURL}/users/update`;
export const LOGOUTUSER = `${APIURL}/users/logout`;

export const GETPROJECTS = `${APIURL}/projects/me`;
export const GETONEPROJECT = `${APIURL}/projects`;
export const CREATEPROJECT = `${APIURL}/projects/create`;
