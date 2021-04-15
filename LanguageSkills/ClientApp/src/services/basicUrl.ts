
export const basicUrl: any = {
  apiUrl:  `${window.location.protocol}//${window.location.hostname}`.includes("localhost") ?
    `${window.location.protocol}//${window.location.hostname}:5001`
    : `${window.location.protocol}//${window.location.hostname}`
};
