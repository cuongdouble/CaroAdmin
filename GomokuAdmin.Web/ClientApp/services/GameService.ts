import Result from "@Core/Result";
import { ServiceBase } from "@Core/ServiceBase";
import SessionManager, { IServiceUser } from "@Core/session";
import { IGameModel } from "@Models/IGameModel";
import { GUID } from "@Models/GuidType";
import { IChatModel } from "../models/IChatModel";

export default class GameService extends ServiceBase {

    public async search(id?: GUID): Promise<Result<IGameModel[]>> {
        var api;
        if (id == null)
            api = `/api/Game/Search`;
        else
            api=`/api/Game/Search?id=${id}`;
        var result = await this.requestJson<IGameModel[]>({
            url: api,
            method: "GET"
        });
        return result;
    }
    
    public async getChat(gameId: GUID): Promise<Result<IChatModel[]>> {
        var result = await this.requestJson<IChatModel[]>({
            url: `api/Game/GetChat?id=${gameId.toString()}`,
            method: "GET",
        });
        return result;
    }
}