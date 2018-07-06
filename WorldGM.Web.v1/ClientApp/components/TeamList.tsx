import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import 'isomorphic-fetch';
import { DefaultComponent, TeamViewModel } from '../WorldGM';

export class TeamList extends DefaultComponent<TeamViewModel> {

    constructor() {
        super();
        
        fetch('api/teams')
            .then(response => {
                return response.json();
            })
            .then(data => {
                this.setState({ values: data, loading: false });
            });
    }

    public render() {
        let teams = this.state.values;
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : <div>
                <table className="table">
                    <thead>
                        <tr>
                            <th>Team</th>
                        </tr>
                    </thead>
                    <tbody>
                        {
                            teams.map(x =>
                                <tr>
                                    <td><a href={'/team/' + x.id}>{x.city} {x.name}</a></td>
                                </tr>
                            )
                        }
                    </tbody>
                </table>
            </div>;

        return <div>{contents}</div>;
    }
}