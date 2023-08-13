const fetchRequest = async (
  url: string,
  requestOptions: RequestInit
): Promise<object> => {
  try {
    const response = await fetch(url, requestOptions);

    if (!response.ok) {
      throw new Error("Invalid details");
    }

    const contentType = response.headers.get("Content-Type");

    let responseData = {};

    if (contentType && contentType.includes("application/json")) {
      responseData = await response.json();
    } else if (contentType && contentType.includes("text/plain")) {
      const textData = await response.text();
      responseData = { text: textData };
    } else {
      throw new Error("Invalid content type");
    }

    return responseData;
  } catch (err) {
    let message = err instanceof Error ? err.message : null;
    return { error: message ? message : "Something went wrong" };
  }
};

export default fetchRequest;
