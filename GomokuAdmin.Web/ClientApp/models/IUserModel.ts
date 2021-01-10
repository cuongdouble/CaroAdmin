import { GUID } from "@Models/GuidType";

export interface IUserModel {
    id: GUID;
    username: string;
    password: string;
    name: string;
    rank: number;
    createAt: Date;
    updateAt: Date;
    numberOfMatches: number;
    numberOfWonMatches: number;
    firstName: string;
    lastName: string;
    photoUrl: string;
}
