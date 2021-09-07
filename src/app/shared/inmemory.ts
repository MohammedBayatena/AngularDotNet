let inMemoryToken: { token: string };

function setSession(inMemoryToken: { token: string }) {
  localStorage.setItem('token', inMemoryToken.token);
}

export function saveToken(jwt_token: string, noRedirect: boolean) {
  inMemoryToken = {
    token: jwt_token,
  };
  setSession(inMemoryToken);
}

export function getToken(): string | null {
  const mytoken = localStorage.getItem('token');
  return inMemoryToken != undefined
    ? 'Bearer ' + inMemoryToken.token
    : mytoken != undefined
    ? 'Bearer ' + mytoken
    : null;
}
