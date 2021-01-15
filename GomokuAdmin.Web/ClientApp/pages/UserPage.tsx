import "@Styles/main.scss";
import * as React from "react";
import { Helmet } from "react-helmet";
import { RouteComponentProps, withRouter } from "react-router";
import * as userStore from "@Store/userStore";
import { withStore } from "@Store/index";
import Paginator from "@Components/shared/Paginator";
import AwesomeDebouncePromise from "awesome-debounce-promise";
import { paginate, getPromiseFromActionCreator } from "@Utils";
import { Modal, Button, Container, Row, Card } from "react-bootstrap";
import { wait } from "domain-wait";
import Result from "@Core/Result";
import { IUserModel } from "@Models/IUserModel";
import { FastField, Field } from "formik";
import { LinkContainer } from 'react-router-bootstrap'
import { Redirect, NavLink, Link } from "react-router-dom";
import { Nav, Navbar, Dropdown } from "react-bootstrap";
import { GUID } from "../models/GuidType";

type Props = typeof userStore.actionCreators & userStore.IUserStoreState & RouteComponentProps<{}>;

interface IState {
    searchTerm: string;
    currentPageNum: number;
    limitPerPage: number;
    isDetailModalOpen: boolean;
    isBanModalOpen: boolean;
    modelForDetail?: IUserModel;
}

class UserPage extends React.Component<Props, IState> {

    private paginator: Paginator;

    private debouncedSearch: (term: string) => void;

    constructor(props: Props) {
        super(props);

        this.state = {
            searchTerm: "",
            currentPageNum: 1,
            limitPerPage: 5,
            isDetailModalOpen: false,
            isBanModalOpen: false,
            modelForDetail: null,
        };

        // "AwesomeDebouncePromise" makes a delay between
        // the end of input term and search request.
        this.debouncedSearch = AwesomeDebouncePromise((term: string) => {
            props.search(term);
        }, 500);
        wait(async () => {
            // Lets tell Node.js to wait for the request completion.
            // It's necessary when you want to see the fethched data 
            // in your prerendered HTML code (by SSR).
            await this.props.search();
        }, "userPageTask");
    }


    private toggleBanUserModal = (modelForDetail?: IUserModel) => {
        this.setState(prev => ({
            modelForDetail,
            isBanModalOpen: !prev.isBanModalOpen
        }));
    }

    private onDetailUserModal = (modelForDetail?: IUserModel) => {
        this.setState({
            modelForDetail: modelForDetail,
            isDetailModalOpen: true
        });
    }

    private offDetailUserModal = () => {
        this.setState({
            modelForDetail: null,
            isDetailModalOpen: false
        });
    }

    private banUser = async (data: IUserModel) => {
        var result = await getPromiseFromActionCreator(this.props.ban(data));
        if (!result.hasErrors) {
            this.toggleBanUserModal(data);
        }
    }

    private getBan(date: Date) {
        if (date == null)
            return "No";
        else return new Date(date.toString()).toLocaleString();
    }

    private linkMacth(id : GUID) {
        return '/games?id=' + id ;
    }

    private renderRows = (data: IUserModel[]) => {
        return paginate(data, this.state.currentPageNum, this.state.limitPerPage)
            .map(user =>
                <tr key={user.id}>
                    <td>{user.username}</td>
                    <td>{user.name}</td>
                    <td>{user.email}</td>
                    <td>{user.numberOfMatches}</td>
                    <td>{user.rank}</td>
                    <td>{this.getBan(user.bannedAt)}</td>
                    <td>
                        <button className="btn btn-info" onClick={x => this.onDetailUserModal(user)}>Detail</button>&nbsp;
                        <button className="btn btn-danger" onClick={x => this.toggleBanUserModal(user)}>
                            {user.bannedAt == null ? "Ban" : "Unban"}
                        </button>&nbsp;
                        <button className="btn btn-light" >
                            <Link to={this.linkMacth(user.id)}>Match History
                            </Link>
                        </button>
                    </td>
                </tr>
            );
    }

    private onChangeSearchInput = (e: React.ChangeEvent<HTMLInputElement>) => {
        var val = e.currentTarget.value;
        this.debouncedSearch(val);
        this.paginator.setFirstPage();
    }

    render() {

        return <Container>
            <Helmet>
                <title>All Users</title>
            </Helmet>

            <Card body className="mt-4 mb-4">
                <Row>
                    <div className="col-9 col-sm-10 col-md-10 col-lg-11">
                        <input
                            type="text"
                            className="form-control"
                            defaultValue={""}
                            onChange={this.onChangeSearchInput}
                            placeholder={"Search for username or name or email ..."}
                        />
                    </div>
                </Row>
            </Card>

            <table className="table">
                <thead>
                    <tr>
                        <th>User Name</th>
                        <th>Name</th>
                        <th>Email</th>
                        <th>Number of Matches</th>
                        <th>Rank</th>
                        <th>Banned</th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    {this.renderRows(this.props.collection)}
                </tbody>
            </table>


            {/* Detail modal */}
            <Modal show={this.state.isDetailModalOpen} onHide={() => this.offDetailUserModal()}>
                <Modal.Header closeButton>
                    <Modal.Title>Information: {this.state.modelForDetail ? `${this.state.modelForDetail.username}` : null}</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <table className="table">
                        <tr>
                            <th>ID</th>
                            <td>{this.state.modelForDetail?.id}</td>
                        </tr>
                        <tr>
                            <th>UserName</th>
                            <td>{this.state.modelForDetail?.username}</td>
                        </tr>
                        <tr>
                            <th>Name</th>
                            <td>{this.state.modelForDetail?.name}</td>
                        </tr>
                        <tr>
                            <th>FirstName</th>
                            <td>{this.state.modelForDetail?.firstName}</td>
                        </tr>
                        <tr>
                            <th>LastName</th>
                            <td>{this.state.modelForDetail?.lastName}</td>
                        </tr>
                        <tr>
                            <th>Create At</th>
                            <td>{new Date(this.state.modelForDetail?.createdAt?.toString()).toLocaleString()}</td>
                        </tr>
                        <tr>
                            <th>Last Updated</th>
                            <td>{new Date(this.state.modelForDetail?.updatedAt?.toString()).toLocaleString()}</td>
                        </tr>
                        <tr>
                            <th>Rank</th>
                            <td>{this.state.modelForDetail?.rank}</td>
                        </tr>
                        <tr>
                            <th>Total matchs</th>
                            <td>{this.state.modelForDetail?.numberOfMatches}</td>
                        </tr>
                        <tr><
                            th>Total win matchs</th>
                            <td>{this.state.modelForDetail?.numberOfWonMatches}</td>
                        </tr>
                        <tr>
                            <th>Image</th>
                            <td> <img src={this.state.modelForDetail?.photoUrl} width={50} height={50} /></td>
                        </tr>
                    </table>
                </Modal.Body>
                <Modal.Footer>
                    <Button variant="primary" onClick={x => this.offDetailUserModal()}>Close</Button>
                </Modal.Footer>
            </Modal>

            {/* Ban modal */}
            <Modal show={this.state.isBanModalOpen} onHide={() => this.toggleBanUserModal()}>
                <Modal.Header closeButton>
                    <Modal.Title>Ban account: {this.state.modelForDetail ? `${this.state.modelForDetail.username}` : null}</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <p>Do you really want to Ban/Unban this account?</p>
                </Modal.Body>
                <Modal.Footer>
                    <Button variant="secondary" onClick={x => this.toggleBanUserModal()}>Close</Button>
                    <Button variant="primary" onClick={x => this.banUser(this.state.modelForDetail)}>
                        {this.state.modelForDetail?.bannedAt == null ? "Ban" : "Unban"}
                    </Button>
                </Modal.Footer>
            </Modal>


            <Paginator
                ref={x => this.paginator = x}
                totalResults={this.props.collection.length}
                limitPerPage={this.state.limitPerPage}
                currentPage={this.state.currentPageNum}
                onChangePage={(pageNum) => this.setState({ currentPageNum: pageNum })} />

        </Container>;
    }
}

function formatTime(time, prefix = "") {
    return typeof time == "object" ? prefix + time.toLocaleDateString() : "";
}

// Connect component with Redux store.
var connectedComponent = withStore(
    UserPage,
    state => state.user, // Selects which state properties are merged into the component's props.
    userStore.actionCreators, // Selects which action creators are merged into the component's props.
);

// Attach the React Router to the component to have an opportunity
// to interract with it: use some navigation components, 
// have an access to React Router fields in the component's props, etc.
export default withRouter(connectedComponent);
