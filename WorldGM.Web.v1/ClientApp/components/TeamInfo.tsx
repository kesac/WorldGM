import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import 'isomorphic-fetch';
import { DefaultComponent, TeamViewModel } from '../WorldGM';

export class TeamInfo extends DefaultComponent<TeamViewModel> {

    constructor() {
        super();
    }

    public componentDidMount() {
        var teamId = this.props.match.params.id;
        fetch('api/team/' + teamId)
            .then(response => {
                return response.json();
            })
            .then(data => {
                this.setState({ values: data, loading: false });
            });
    }

    public render() {

        if (this.state.loading) {
            return <div><p><em>Loading...</em></p></div>
        }
        else {

            let team = this.state.values[0];
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