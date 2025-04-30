import dayjs from "dayjs";
import { Notify } from "quasar";
const { serializeError } = require("serialize-error");

const handleError = function (error) {
  const serialized = serializeError(error);
  if (error.hasOwnProperty("response")) {
    if (error.response == null) {
      Notify.create({
        message: "Error Performing Action, Please Try Again!",
        color: "negative",
        icon: "warning"
      });
      return;
    }
    if (error.response.status == 401) {
      Notify.create({
        message: "Session Expired",
        color: "negative",
        icon: "warning"
      });
      return;
    }
    let eror = error?.response?.data?.errors
    let firstPropertyKey = Object.keys(eror)[0];
    let firstPropertyValue = eror[firstPropertyKey][0];
    if (firstPropertyValue)
    {
      Notify.create({
        message: firstPropertyValue,
        color: "negative",
        icon: "warning",
      });
    }
    else
    {
      Notify.create({
      message: error.response.data,
      color: "negative",
      icon: "warning",
    });
    }
  } else if (serialized.hasOwnProperty("message")) {
    Notify.create({
      message: serialized.message,
      color: "negative",
      icon: "warning",
    });
  }
  else
  Notify.create({
    message: error.response.data,
    color: "negative",
    icon: "warning",
  });
};

const dateFormat = function (date, format = 'YYYY-MM-DD') {
  return dayjs(`${date}`).format(format);
};

export { handleError, dateFormat };
