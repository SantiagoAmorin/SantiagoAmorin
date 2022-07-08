// Creo el estado por defecto e inicial que va a tener mi app
import { ON_LOGIN, ON_LOGOUT } from './constants';
const initialState = {
  user: null,
  paquetes: [],
};

// Creo mi función reductora, con el state inicial
const appReducer = (state = initialState, action) => {
  const { type, payload } = action;

  switch (type) {
    case ON_LOGIN:
      return { ...state, user: payload.user };
    case ON_LOGOUT:
      return { ...state, user: null };
    default:
      return state;
  }
};

export default appReducer;
