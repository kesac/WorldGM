import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import 'isomorphic-fetch';

interface AthleteViewModel {
    id: number;
    name: string;
    position: string;
    age: number;
    overallRating: number;
    hasContract: boolean;
    teamId: number;
    teamName: string;
    contractFirstYear: number;
    contractLastYear: number;
    annualPay: number;
}

interface TeamViewModel {
    id: number;
    city: string;
    name: string;
    athletes: AthleteViewModel[];
}

interface TeamInfoState {
    teams: TeamViewModel[];
    loading: boolean;
}

interface TeamInfoProps extends RouteComponentProps<any> {
    id: number;
}

export class TeamInfo extends React.Component<TeamInfoProps, TeamInfoState> {

    constructor() {
        super();
        this.state = { teams: [], loading: true };
    }

    public componentDidMount() {
        var teamId = this.props.match.params.id;
        fetch('api/team/' + teamId)
            .then(response => {
                return response.json();
            })
            .then(data => {
                this.setState({ teams: data, loading: false });
            });
    }

    public render() {

        if (this.state.loading) {
            return <div><p><em>Loading...</em></p></div>
        }
        else {

            let team = this.state.teams[0];
            let athletes = team.athletes;
            let contents = <div>
                    <h1>{team.city} {team.name}</h1>
                    <h2>Players</h2>
                    <table className="table">
                        <thead>
                            <tr>
                                <th>Position</th>
                                <th>Name</th>
                                <th>Age</th>
                                <th>Overall</th>
                                <th>Start</th>
                                <th>End</th>
                                <th>Pay</th>
                            </tr>
                        </thead>
                        <tbody>
                            {
                                athletes.map(x =>
                                    <tr>
                                        <td>{x.position}</td>
                                        <td><a href={'/athlete/' + x.id}>{x.name}</a></td>
                                        <td>{x.age}</td>
                                        <td>{x.overallRating}</td>
                                        <td>{x.contractFirstYear}</td>
                                        <td>{x.contractLastYear}</td>
                                        <td>{x.annualPay}</td>
                                    </tr>
                                )
                            }
                        </tbody>
                    </table>
                </div>;

            return <div>{contents}</div>;
        }
    }
}