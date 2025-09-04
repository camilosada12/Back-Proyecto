export interface User{
    id: number;
    name: string;
    email: string;
    password?: string;
    passwordHash?: string;
}
export interface UserCreate{
    id: number;
    name: string;
    email: string;
    password?: string;
    passwordHash?: string;
}