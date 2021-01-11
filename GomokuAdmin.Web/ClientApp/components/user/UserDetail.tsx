import * as React from "react";
import { IUserModel } from "@Models/IUserModel";


const UserDetail = (props: IUserModel) => {
        return
        (<table className="table">
            <tr>
                <th>UserName</th>
                <td>{}</td>
            </tr>
            <tr>
                <th>Name</th>
                <td>{props.name}</td>
            </tr>
            <tr>
                <th>FirstName</th>
                <td>{props.firstName}</td>
            </tr>
            <tr>
                <th>LastName</th>
                <td>{props.lastName}</td>
            </tr>
            <tr>
                <th>Create At</th>
                <td>{props.createdAt}</td>
            </tr>
            <tr>
                <th>Last Updated</th>
                <td>{props.updatedAt}</td>
            </tr>
            <tr>
                <th>Rank</th>
                <td>{props.rank}</td>
            </tr>
            <tr>
                <th>Total matchs</th>
                <td>{props.numberOfMatches}</td>
            </tr>
            <tr><
                th>Total win matchs</th>
                <td>{props.numberOfWonMatches}</td>
            </tr>
            <tr>
                <th>Image</th>
                <td> <img src={props.photoUrl} width={50} height={50} /></td>
            </tr>
        </table>);
}
export default UserDetail;