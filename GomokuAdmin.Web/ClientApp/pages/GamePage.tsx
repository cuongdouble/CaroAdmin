import "@Styles/main.scss";
import * as React from "react";
import * as Enum from "@Models/EnumType";
import { Helmet } from "react-helmet";
import { RouteComponentProps, withRouter } from "react-router";
import { withStore } from "@Store/index";
import Paginator from "@Components/shared/Paginator";
import AwesomeDebouncePromise from "awesome-debounce-promise";
import { paginate, getPromiseFromActionCreator } from "@Utils";
import { Modal, Button, Container, Row, Card } from "react-bootstrap";
import { wait } from "domain-wait";
import Result from "@Core/Result";
import { IGameModel } from "@Models/IGameModel";
import * as gameStore from "@Store/gameStore";
import { GUID } from "../models/GuidType";
import { ChatComponent } from "../components/game/chat";

type Props = typeof gameStore.actionCreators & gameStore.IGameStoreState & RouteComponentProps<{}>;

interface IState {
    searchTerm: string;
    currentPageNum: number;
    limitPerPage: number;
    isChatModelOpen: boolean;
    gameModelDetail?: IGameModel;
    userId: string;
}

class GamePage extends React.Component<Props, IState> {

    private paginator: Paginator;

    private debouncedSearch: (term: string) => void;

    private guid(guid: string): GUID {
    return guid as GUID; // maybe add validation that the parameter is an actual guid ?
}

    constructor(props: Props) {
        super(props);

        this.state = {
            searchTerm: this.getQuery(),
            currentPageNum: 1,
            limitPerPage: 5,
            isChatModelOpen: false,
            gameModelDetail: null,
            userId: this.getQuery(),
        };

        wait(async () => {
            // Lets tell Node.js to wait for the request completion.
            // It's necessary when you want to see the fethched data 
            // in your prerendered HTML code (by SSR).
                await this.props.search(this.guid(this.state.userId));
        }, "gamePageTask");
    }


    private toggleChatModal = (gameModelDetail?: IGameModel) => {
        this.setState(prev => ({
            gameModelDetail,
            isChatModelOpen: !prev.isChatModelOpen
        }));
    }


    private getQuery = () => {
        const query = new URLSearchParams(this.props.location.search);
        return query.get("id");
    }

    private renderRows = (data: IGameModel[]) => {
        if (data.length==0) {
            return <p>No Match Found</p>;
        }
        return paginate(data, this.state.currentPageNum, this.state.limitPerPage)
            .map(game =>
                <tr key={game.id}>
                    <td>{game.boardSize}</td>
                    <td>{Enum.getGameType(game.gameType)}</td>
                    <td>{Enum.getGameResult(game.gameResult)}</td>
                    <td>{game.duration}</td>
                    <td>{new Date(game.startAt?.toString()).toLocaleString()}</td>
                    <td>
                        <button className="btn btn-info" onClick={x => this.toggleChatModal(game)}>SeeChat</button>&nbsp;
                    </td>
                </tr>
            );
    }

    render() {

        return <Container>
            <Helmet>
                <title>{this.state.userId == null ? 'All matchs' : `User matchs`}</title>
            </Helmet>

            <table className="table">
                <thead>
                    <tr>
                        <th>Board Size</th>
                        <th>Game Result Type</th>
                        <th>Game Result</th>
                        <th>Duration</th>
                        <th>Time</th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    {this.renderRows(this.props.collection)}
                </tbody>
            </table>


            <Paginator
                ref={x => this.paginator = x}
                totalResults={this.props.collection.length}
                limitPerPage={this.state.limitPerPage}
                currentPage={this.state.currentPageNum}
                onChangePage={(pageNum) => this.setState({ currentPageNum: pageNum })} />


            {/* Update modal */}
            <Modal show={this.state.isChatModelOpen} onHide={() => this.toggleChatModal()}>
                <Modal.Header closeButton>
                    <Modal.Title>Chat in game </Modal.Title>
                </Modal.Header>
                <ChatComponent id={this.state.gameModelDetail?.id} />
                <Modal.Footer>
                    <Button variant="secondary" onClick={x => this.toggleChatModal()}>Close</Button>
                </Modal.Footer>
            </Modal>

        </Container>;
    }
}


// Connect component with Redux store.
var connectedComponent = withStore(
    GamePage,
    state => state.game, // Selects which state properties are merged into the component's props.
    gameStore.actionCreators, // Selects which action creators are merged into the component's props.
);

// Attach the React Router to the component to have an opportunity
// to interract with it: use some navigation components, 
// have an access to React Router fields in the component's props, etc.
export default withRouter(connectedComponent);
