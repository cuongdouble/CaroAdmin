import { GUID } from "@Models/GuidType";

export interface IGameModel {
    id: GUID;
    boardSize: number;
    startAt: Date;
    chatId: null | GUID;
    gameResult: number | null;
    duration: number | null;
    winningLine: null;
    gameType: number;
    chat: null;
    moveRecords: any[];
    rankRecords: any[];
    teams: any[];
}
