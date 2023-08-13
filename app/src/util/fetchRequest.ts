const fetchRequest = async (
  url: string,
  requestOptions: RequestInit
) => {
  try {
    const response = await fetch(url, requestOptions);

    if (!response.ok) {
      throw new Error("Invalid details");
    }

    return response;
  } catch (err) {
    let message = err instanceof Error ? err.message : null;
    return { error: message ? message : "Something went wrong" };
  }
};

export default fetchRequest;
