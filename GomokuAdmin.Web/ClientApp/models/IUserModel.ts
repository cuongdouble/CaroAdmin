import { GUID } from "@Models/GuidType";

export interface IUserModel {
    id: GUID;
    username: string;
    password: null | string;
    name: string;
    createdAt: Date;
    updatedAt: Date;
    rank: number;
    numberOfMatches: number;
    numberOfWonMatches: number;
    firstName: string;
    lastName: string;
    photoUrl: string;
    email: string;
    resetPasswordToken: null;
    resetPasswordExpires: null;
    activateCode: null;
    activatedAt: null;
    bannedAt: Date;
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