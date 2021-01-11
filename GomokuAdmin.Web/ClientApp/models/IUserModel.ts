import { GUID } from "@Models/GuidType";

export interface IUserModel {
    id: GUID;
    username: string;
    password: string;
    name: string;
    rank: number;
    createdAt: Date;
    updatedAt: Date;
    numberOfMatches: number;
    numberOfWonMatches: number;
    firstName: string;
    lastName: string;
    photoUrl: string;
    chatParticipants: any[];
    chatRecords: any[];
    friendParticipantUser1s: any[];
    friendParticipantUser2s: any[];
    friendRequestReceivers: any[];
    friendRequestSenders: any[];
    moveRecords: any[];
    rankRecords: any[];
    teamParticipants: any[];
}