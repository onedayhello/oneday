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
  credentials: "include"
};

const Auth = {
  isLoggedIn: () => {
    const token = localStorage.getItem("token");
  },
  login: async (body: RequestBody) => {
    const url = "https://localhost:7095/Users/login";

    requestOptions.body = JSON.stringify(body);

    const responseData = await fetch(url, requestOptions);

    if (!responseData || !responseData.ok) {
        console.log("not ok")
      return null;
    }

    const token = await responseData.text();

    localStorage.setItem("token", token);

    console.log(token);

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
