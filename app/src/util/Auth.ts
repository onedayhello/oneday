import fetchRequest from "./fetchRequest";

interface RequestBody {
  username: string;
  password: string;
}

const headers = new Headers();
headers.append("Content-type", "application/json");

const requestOptions: RequestInit = {
  method: "POST",
  headers: headers,
};

const Auth = {
  isLoggedIn: () => {
    const token = localStorage.getItem("token");
  },
  login: async (body: RequestBody) => {
    const url = "https://localhost:7095/Users/login";

    requestOptions.body = JSON.stringify(body);

    const responseData = (await fetchRequest(url, requestOptions)) as {
      text: string;
    };

    localStorage.setItem("token", responseData.text);

    console.log(responseData);

    return responseData;
  },
  logout: () => {
    localStorage.removeItem("token");
  },
  signup: async (body: RequestBody) => {
    const url = "https://localhost:7095/Users/signup";

    requestOptions.body = JSON.stringify(body);

    const responseData = await fetchRequest(url, requestOptions);

    return responseData;
  },
};

export default Auth;
