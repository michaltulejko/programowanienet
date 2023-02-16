import { User } from './user';

export interface UserSession {
  token: string;
  user: User;
}
