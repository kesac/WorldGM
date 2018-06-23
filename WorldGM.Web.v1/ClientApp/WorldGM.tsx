import * as React from 'react';
import { RouteComponentProps } from 'react-router';

/* Maps to C# model classes */
export interface World {
    name: string;
    currentYear: number;
    continents: Continent[];
}

export interface Continent {
    name: string;
    regions: Region[];
}

export interface Region {
    name: string;
    cities: City[];
}

export interface City {
    name: string;
}

/* Maps to C# viewmodel classes */
export interface AthleteViewModel {
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

export interface TeamViewModel {
    id: number;
    city: string;
    name: string;
    athletes: AthleteViewModel[];
}

/* component states */
export interface DefaultState<T> {
    values: T[];
    loading: boolean;
}

/* component props */
export interface DefaultProps extends RouteComponentProps<any> {
    id: number;
}

/* parent component */
export class DefaultComponent<T> extends React.Component<DefaultProps, DefaultState<T>> {
    constructor() {
        super();
        this.state = { values: [], loading: true };
    }
}


