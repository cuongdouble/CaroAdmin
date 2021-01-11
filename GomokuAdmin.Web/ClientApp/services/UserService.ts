import Result from "@Core/Result";
import { ServiceBase } from "@Core/ServiceBase";
import SessionManager, { IServiceUser } from "@Core/session";
import { IUserModel} from "@Models/IUserModel";
import { GUID } from "@Models/GuidType";

export default class UserService extends ServiceBase {

    public async search(term: string = null): Promise<Result<IUserModel[]>> {
        if (term == null) {
            term = "";
        }
        var result = await this.requestJson<IUserModel[]>({
            url: `/api/User/Search?term=${term}`,
            method: "GET"
        });
        return result;
    }

    //public async banChat(userId: GUID): Promise<Result<{}>> {
    //    var result = await this.requestJson({
    //        url: `/api/User/${userId}`,
    //        method: "PATCH",
    //        data: userId
    //    });
    //    return result;
    //}


}